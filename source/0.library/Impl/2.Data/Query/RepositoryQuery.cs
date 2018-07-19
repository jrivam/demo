using library.Impl.Data.Sql;
using library.Impl.Data.Sql.Builder;
using library.Impl.Data.Sql.Factory;
using library.Impl.Entities.Repository;
using library.Interface.Data;
using library.Interface.Data.Mapper;
using library.Interface.Data.Query;
using library.Interface.Data.Sql;
using library.Interface.Data.Table;
using library.Interface.Entities;
using library.Interface.Entities.Reader;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace library.Impl.Data.Query
{
    public class RepositoryQuery<S, T, U> : Repository<T>, IRepositoryQuery<S, T, U> 
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where S : IQueryRepositoryMethods<T, U>
    {
        protected readonly IMapperRepository<T, U> _mapper;
        protected readonly ISqlSyntaxSign _syntaxsign;
        protected readonly ISqlCommandBuilder _commandbuilder;
        protected readonly ISqlBuilderQuery _builder;

        public RepositoryQuery(ISqlCreator creator, IReaderEntity<T> reader,
            IMapperRepository<T, U> mapper,
            ISqlSyntaxSign syntaxsign,
            ISqlCommandBuilder commandbuilder, ISqlBuilderQuery builder)
            : base(creator, reader)
        {
            _mapper = mapper;
            _syntaxsign = syntaxsign;
            _builder = builder;
            _commandbuilder = commandbuilder;
        }

        public RepositoryQuery(ISqlCreator creator, IReaderEntity<T> reader,
            IMapperRepository<T, U> mapper,
            ISqlSyntaxSign syntaxsign,
            ISqlCommandBuilder commandbuilder)
            : this(creator, reader,
                  mapper,
                  syntaxsign,
                  commandbuilder,
                  new SqlBuilderQuery(syntaxsign))
        {
        }
        public RepositoryQuery(IReaderEntity<T> reader, 
            IMapperRepository<T, U> mapper, 
            ConnectionStringSettings connectionstringsettings)
            : this(new SqlCreator(connectionstringsettings), reader,
                  mapper,
                  SqlSyntaxSignFactory.Create(connectionstringsettings),
                  SqlCommandBuilderFactory.Create(connectionstringsettings),
                  new SqlBuilderQuery(SqlSyntaxSignFactory.Create(connectionstringsettings)))
        {
        }
        public RepositoryQuery(IReaderEntity<T> reader, 
            IMapperRepository<T, U> mapper, 
            string appconnectionstringname)
            : this(reader, 
                  mapper, 
                  ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appconnectionstringname]])
        {
        }

        public virtual (Result result, U data) 
            SelectSingle
            (IQueryRepository query,
            int maxdepth = 1, 
            U data = default(U))
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
            int maxdepth = 1, U 
            data = default(U))
        {
            return SelectSingle(_creator.GetCommand(commandtext, commandtype, parameters), maxdepth, data);
        }

        public virtual (Result result, U data) 
            SelectSingle
            (IDbCommand command, 
            int maxdepth = 1, 
            U data = default(U))
        {
            var executequery = ExecuteQuery(command, _syntaxsign.AliasSeparatorColumn, maxdepth, data != null && data.Entity != null ? new List<T> { data.Entity } : default(List<T>));

            if (executequery.result.Success && executequery.entities != null)
            {
                data = _mapper.CreateInstance(executequery.entities.FirstOrDefault());

                _mapper.Clear(data, 1, 0);
                _mapper.Map(data, 1, 0);

                return (executequery.result, data);
            }

            return (executequery.result, default(U));
        }

        public virtual (Result result, IEnumerable<U> datas) 
            SelectMultiple
            (IQueryRepository query,
            int maxdepth = 1, int top = 0,
            IListData<T, U> datas = null)
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
            int maxdepth = 1,
            IListData<T, U> datas = null)
        {
            return SelectMultiple(_creator.GetCommand(commandtext, commandtype, parameters), maxdepth, datas);
        }

        public virtual (Result result, IEnumerable<U> datas) 
            SelectMultiple
            (IDbCommand command, 
            int maxdepth = 1,
            IListData<T, U> datas = null)
        {
            var enumeration = new List<U>();
            var iterator = (datas ?? new ListData<S, T, U>()).GetEnumerator();

            var executequery = ExecuteQuery(command, _syntaxsign.AliasSeparatorColumn, maxdepth, datas?.Entities);

            if (executequery.result.Success && executequery.entities != null)
            {
                foreach (var entity in executequery.entities)
                {
                    var data = iterator.MoveNext() ? iterator.Current : _mapper.CreateInstance(entity);

                    _mapper.Clear(data, maxdepth, 0);
                    _mapper.Map(data, maxdepth, 0);

                    enumeration.Add(data);
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
                _builder.GetUpdateSet(tablecolumns.Where(c => !c.column.IsIdentity && c.column.Value != c.column.DbValue).ToList(), parameters),
                _builder.GetWhere(querycolumns, parameters));

            return Update(update, CommandType.Text, parameters);
        }

        public virtual (Result result, int rows) 
            Update
            (string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null)
        {
            return Update(_creator.GetCommand(commandtext, commandtype, parameters));
        }

        public virtual (Result result, int rows) 
            Update
            (IDbCommand command)
        {
            return ExecuteNonQuery(command);
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
            return Delete(_creator.GetCommand(commandtext, commandtype, parameters));
        }

        public virtual (Result result, int rows) 
            Delete
            (IDbCommand command)
        {
            return ExecuteNonQuery(command);
        }
    }
}
