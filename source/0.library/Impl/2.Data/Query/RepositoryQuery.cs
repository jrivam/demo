using library.Impl.Data.Sql;
using library.Impl.Data.Sql.Builder;
using library.Impl.Data.Sql.Factory;
using library.Interface.Data.Mapper;
using library.Interface.Data.Query;
using library.Interface.Data.Sql;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace library.Impl.Data.Query
{
    public class RepositoryQuery<T, U> : Repository<T, U>, IRepositoryQuery<T, U> 
        where T : IEntity
        where U : ITableRepositoryProperties<T>
    {
        protected readonly ISqlBuilderQuery _builder;
        protected readonly ISqlCommandBuilder _commandbuilder;

        public RepositoryQuery(ISqlCreator creator, IMapperRepository<T, U> mapper, 
            ISqlBuilderQuery builder, ISqlCommandBuilder commandbuilder)
            : base(creator, mapper)
        {
            _builder = builder;
            _commandbuilder = commandbuilder;
        }
        public RepositoryQuery(IMapperRepository<T, U> mapper, ConnectionStringSettings connectionstringsettings)
            : this(new SqlCreator(connectionstringsettings), mapper, 
                  new SqlBuilderQuery(SqlSyntaxSignFactory.Create(connectionstringsettings)),
                  SqlCommandBuilderFactory.Create(connectionstringsettings))
        {
        }
        public RepositoryQuery(IMapperRepository<T, U> mapper, string appconnectionstringname)
            : this(mapper, ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appconnectionstringname]])
        {
        }

        public virtual (Result result, U data) 
            SelectSingle
            (IQueryRepositoryProperties query,
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
            var executequery = ExecuteQuery(command, maxdepth, data != null ? new List<U> { data } : default(List<U>));

            return (executequery.result, executequery.datas.FirstOrDefault());
        }

        public virtual (Result result, IEnumerable<U> datas) 
            SelectMultiple
            (IQueryRepositoryProperties query,
            int maxdepth = 1, int top = 0, 
            IList<U> datas = null)
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
            IList<U> datas = null)
        {
            return SelectMultiple(_creator.GetCommand(commandtext, commandtype, parameters), maxdepth, datas);
        }

        public virtual (Result result, IEnumerable<U> datas) 
            SelectMultiple
            (IDbCommand command, 
            int maxdepth = 1, 
            IList<U> datas = null)
        {
            return ExecuteQuery(command, maxdepth, datas);
        }

        public virtual (Result result, int rows) 
            Update
            (IQueryRepositoryProperties query,
            IList<ITableColumn> columns,
            int maxdepth = 1)
        {
            var parameters = new List<SqlParameter>();

            var querycolumns = _builder.GetQueryColumns(query, null, null, maxdepth, 0);
            var queryjoins = _builder.GetQueryJoins(query, new List<string>() { query.Description.Name }, maxdepth, 0);

            var update = _commandbuilder.Update($"{query.Description.Name}",
                _builder.GetFrom(queryjoins, query.Description.Name),
                _builder.GetUpdateSet(columns.Where(c => !c.IsIdentity && c.Value != c.DbValue).ToList(), parameters),
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
            (IQueryRepositoryProperties query,
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
