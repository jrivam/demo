using library.Impl.Data.Sql;
using library.Impl.Data.Sql.Builder;
using library.Impl.Data.Sql.Factory;
using library.Impl.Entities.Repository;
using library.Interface.Data;
using library.Interface.Data.Mapper;
using library.Interface.Data.Query;
using library.Interface.Data.Sql;
using library.Interface.Data.Sql.Builder;
using library.Interface.Data.Table;
using library.Interface.Entities;
using library.Interface.Entities.Reader;
using library.Interface.Entities.Repository;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace library.Impl.Data.Query
{
    public class RepositoryQuery<S, T, U> : IRepositoryQuery<S, T, U> 
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where S : IQueryRepositoryMethods<T, U>
    {
        protected readonly ISqlBuilderQuery _builder;

        protected readonly IRepository<T> _repository;
        protected readonly IRepositoryBulk _repositorybulk;

        protected readonly IMapperRepository<T, U> _mapper;
        protected readonly ISqlSyntaxSign _syntaxsign;
        protected readonly ISqlCommandBuilder _commandbuilder;

        public RepositoryQuery(ISqlBuilderQuery builder,
            IRepository<T> repository, IRepositoryBulk repositorybulk, 
            IMapperRepository<T, U> mapper, ISqlSyntaxSign syntaxsign, ISqlCommandBuilder commandbuilder)
        {
            _builder = builder;

            _repository = repository;
            _repositorybulk = repositorybulk;

            _mapper = mapper;
            _syntaxsign = syntaxsign;
            _commandbuilder = commandbuilder;
        }

        public RepositoryQuery(IRepository<T> repository, IRepositoryBulk repositorybulk,
            IMapperRepository<T, U> mapper, ISqlSyntaxSign syntaxsign, ISqlCommandBuilder commandbuilder)
            : this(new SqlBuilderQuery(syntaxsign),
                  repository, repositorybulk,
                  mapper, syntaxsign, commandbuilder)
        {
        }
        public RepositoryQuery(IReaderEntity<T> reader, IMapperRepository<T, U> mapper,
            ISqlCreator creator, ISqlSyntaxSign syntaxsign, ISqlCommandBuilder commandbuilder)
            : this(new Repository<T>(creator, reader), new RepositoryBulk(creator),
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
            (IQueryRepository query,
            int maxdepth = 1)
        {
            var parameters = new List<SqlParameter>();

            var querycolumns = _builder.GetQueryColumns(query, null, null, maxdepth, 0);
            var queryjoins = _builder.GetQueryJoins(query, new List<string>() { query.Description.Name }, maxdepth, 0);

            var select = _commandbuilder.Select(_builder.GetSelectColumns(querycolumns),
                _builder.GetFrom(queryjoins, query.Description.Name),
                _builder.GetWhere(querycolumns, parameters), 1);

            return SelectSingle(select, CommandType.Text, parameters, maxdepth);
        }

        public virtual (Result result, U data) 
            SelectSingle
            (string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, 
            int maxdepth = 1)
        {
            var executequery = _repository.ExecuteQuery(_syntaxsign.AliasSeparatorColumn, commandtext, commandtype, parameters, maxdepth);

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
            (IQueryRepository query,
            int maxdepth = 1, int top = 0)
        {
            var parameters = new List<SqlParameter>();

            var querycolumns = _builder.GetQueryColumns(query, null, null, maxdepth, 0);
            var queryjoins = _builder.GetQueryJoins(query, new List<string>() { query.Description.Name }, maxdepth, 0);

            var select = _commandbuilder.Select(_builder.GetSelectColumns(querycolumns),
                _builder.GetFrom(queryjoins, query.Description.Name),
                _builder.GetWhere(querycolumns, parameters), top);

            return SelectMultiple(select, CommandType.Text, parameters, maxdepth);
        }

        public virtual (Result result, IEnumerable<U> datas) 
            SelectMultiple
            (string commandtext,  CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, 
            int maxdepth = 1)
        {
            var enumeration = new List<U>();
            var iterator = new ListData<S, T, U>().GetEnumerator();

            var executequery = _repository.ExecuteQuery(_syntaxsign.AliasSeparatorColumn, commandtext, commandtype, parameters, maxdepth);

            if (executequery.result.Success && executequery.entities != null)
            {
                foreach (var entity in executequery.entities)
                {
                    var instance = iterator.MoveNext() ? iterator.Current : _mapper.CreateInstance(entity);

                    _mapper.Clear(instance, maxdepth, 0);
                    _mapper.Map(instance, maxdepth, 0);

                    enumeration.Add(instance);
                }

                return (executequery.result, enumeration);
            }

            return (executequery.result, default(IList<U>));
        }


        public virtual (Result result, int rows) 
            Update
            (IQueryRepository query,
            IList<(ITableRepository table, ITableColumn column)> tablecolumns,
            int maxdepth = 1)
        {
            var parameters = new List<SqlParameter>();

            var querycolumns = _builder.GetQueryColumns(query, null, null, maxdepth, 0);
            var queryjoins = _builder.GetQueryJoins(query, new List<string>() { query.Description.Name }, maxdepth, 0);

            var update = _commandbuilder.Update($"{query.Description.Name}",
                _builder.GetFrom(queryjoins, query.Description.Name),
                _builder.GetUpdateSet(tablecolumns.Where(c => !c.column.IsIdentity && c.column.Value != c.column.DbValue).ToList(), parameters, _syntaxsign.UpdateSetUseAlias),
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
            (IQueryRepository query,
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
