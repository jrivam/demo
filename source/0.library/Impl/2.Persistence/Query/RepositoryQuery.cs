using library.Impl.Data;
using Library.Impl.Persistence.Sql;
using Library.Impl.Persistence.Sql.Builder;
using Library.Impl.Persistence.Sql.Factory;
using Library.Impl.Persistence.Sql.Repository;
using Library.Impl.Entities;
using Library.Interface.Persistence;
using Library.Interface.Persistence.Mapper;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Sql.Builder;
using Library.Interface.Persistence.Sql.Database;
using Library.Interface.Persistence.Sql.Providers;
using Library.Interface.Persistence.Sql.Repository;
using Library.Interface.Persistence.Table;
using Library.Interface.Entities;
using Library.Interface.Entities.Reader;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace Library.Impl.Persistence.Query
{
    public class RepositoryQuery<T, U> : Repository<T, U>, IRepositoryQuery<T, U> 
        where T : IEntity
        where U : ITableData<T, U>
    {
        protected readonly ISqlBuilderQuery _builder;

        public RepositoryQuery(ISqlBuilderQuery builder,
            ISqlRepository<T> repository, ISqlRepositoryBulk repositorybulk, 
            IMapperRepository<T, U> mapper, ISqlSyntaxSign syntaxsign, ISqlCommandBuilder commandbuilder)
            : base(repository, repositorybulk,
                  mapper, syntaxsign, commandbuilder)
        {
            _builder = builder;
        }

        public RepositoryQuery(ISqlRepository<T> repository, ISqlRepositoryBulk repositorybulk,
            IMapperRepository<T, U> mapper, ISqlSyntaxSign syntaxsign, ISqlCommandBuilder commandbuilder)
            : this(new SqlBuilderQuery(syntaxsign),
                  repository, repositorybulk,
                  mapper, syntaxsign, commandbuilder)
        {
        }
        public RepositoryQuery(IReaderEntity<T> reader, IMapperRepository<T, U> mapper,
            ISqlCreator creator, ISqlSyntaxSign syntaxsign, ISqlCommandBuilder commandbuilder)
            : this(new SqlRepository<T>(creator, reader), new SqlRepositoryBulk(creator),
                  mapper, syntaxsign, commandbuilder)
        {
        }
        public RepositoryQuery(IReaderEntity<T> reader, IMapperRepository<T, U> mapper, 
            ConnectionStringSettings connectionstringsettings)
            : this(reader, mapper,
                  new SqlCreator(connectionstringsettings),
                  SqlSyntaxSignFactory.Create(connectionstringsettings),
                  SqlCommandBuilderFactory.Create(connectionstringsettings))
        {
        }

        public RepositoryQuery(IReaderEntity<T> reader, IMapperRepository<T, U> mapper, 
            string appconnectionstringname)
            : this(reader, mapper, 
                  ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appconnectionstringname]])
        {
        }

        public virtual (Result result, U data) 
            SelectSingle
            (IQueryData<T, U> query,
            int maxdepth = 1, U data = default(U))
        {
            var parameters = new List<SqlParameter>();

            var querycolumns = _builder.GetQueryColumns(query, null, null, maxdepth, 0);
            var queryjoins = _builder.GetQueryJoins(query, new List<string>() { query.Description.Name }, maxdepth, 0);

            var select = _commandbuilder.Select(_builder.GetSelectColumns(querycolumns),
                _builder.GetFrom(queryjoins, query.Description.Name),
                _builder.GetWhere(querycolumns, parameters), 1);

            return SelectSingle(select, CommandType.Text, parameters, maxdepth, data);
        }

        public virtual (Result result, U data) 
            SelectSingle
            (string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, 
            int maxdepth = 1, U data = default(U))
        {
            var executequery = _repository.ExecuteQuery(_syntaxsign.AliasSeparatorColumn, commandtext, commandtype, parameters, maxdepth, (data != null ? new ListEntity<T>() { data.Entity } : null));

            if (executequery.result.Success && executequery.entities != null)
            {
                var instance = _mapper.CreateInstance(executequery.entities.FirstOrDefault());

                _mapper.Clear(instance, 1, 0);
                _mapper.Map(instance, 1, 0);

                _mapper.Extra(instance, 1, 0);

                return (executequery.result, instance);
            }

            return (executequery.result, default(U));
        }

        public virtual (Result result, IEnumerable<U> datas) 
            SelectMultiple
            (IQueryData<T, U> query,
            int maxdepth = 1, int top = 0, IListData<T, U> datas = null)
        {
            var parameters = new List<SqlParameter>();

            var querycolumns = _builder.GetQueryColumns(query, null, null, maxdepth, 0);
            var queryjoins = _builder.GetQueryJoins(query, new List<string>() { query.Description.Name }, maxdepth, 0);

            var select = _commandbuilder.Select(_builder.GetSelectColumns(querycolumns),
                _builder.GetFrom(queryjoins, query.Description.Name),
                _builder.GetWhere(querycolumns, parameters), top);

            return SelectMultiple(select, CommandType.Text, parameters, maxdepth, datas);
        }

        public virtual (Result result, IEnumerable<U> datas) 
            SelectMultiple
            (string commandtext,  CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, 
            int maxdepth = 1, IListData<T,U> datas = null)
        {
            var enumeration = new List<U>();

            var executequery = _repository.ExecuteQuery(_syntaxsign.AliasSeparatorColumn, commandtext, commandtype, parameters, maxdepth, (datas?.Entities != null ? datas?.Entities : new ListEntity<T>()));

            if (executequery.result.Success && executequery.entities != null)
            {
                foreach (var entity in executequery.entities)
                {
                    var instance = _mapper.CreateInstance(entity);

                    _mapper.Clear(instance, maxdepth, 0);
                    _mapper.Map(instance, maxdepth, 0);

                    _mapper.Extra(instance, maxdepth, 0);

                    enumeration.Add(instance);
                }

                return (executequery.result, enumeration);
            }

            return (executequery.result, default(IList<U>));
        }


        public virtual (Result result, int rows) 
            Update
            (IQueryData<T, U> query,
            IList<IColumnTable> columns,
            int maxdepth = 1)
        {
            var parameters = new List<SqlParameter>();

            var querycolumns = _builder.GetQueryColumns(query, null, null, maxdepth, 0);
            var queryjoins = _builder.GetQueryJoins(query, new List<string>() { query.Description.Name }, maxdepth, 0);

            var update = _commandbuilder.Update($"{query.Description.Name}",
                _builder.GetFrom(queryjoins, query.Description.Name),
                _builder.GetUpdateSet(columns.Where(c => !c.IsIdentity && c.Value != c.DbValue).Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), parameters, _syntaxsign.UpdateSetUseAlias),
                _builder.GetWhere(querycolumns, parameters));

            return Update(update, CommandType.Text, parameters);
        }

        public virtual (Result result, int rows) 
            Update
            (string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null)
        {
            return _repositorybulk.ExecuteNonQuery(commandtext, commandtype, parameters);
        }


        public virtual (Result result, int rows) 
            Delete
            (IQueryData<T, U> query,
            int maxdepth = 1)
        {
            var parameters = new List<SqlParameter>();

            var querycolumns = _builder.GetQueryColumns(query, null, null, maxdepth, 0);
            var queryjoins = _builder.GetQueryJoins(query, new List<string>() { query.Description.Name }, maxdepth, 0);

            var delete = _commandbuilder.Delete($"{query.Description.Name}", 
                _builder.GetFrom(queryjoins, query.Description.Name),
                _builder.GetWhere(querycolumns, parameters));

            return Delete(delete, CommandType.Text, parameters);
        }

        public virtual (Result result, int rows) 
            Delete
            (string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null)
        {
            return _repositorybulk.ExecuteNonQuery(commandtext, commandtype, parameters);
        }
    }
}
