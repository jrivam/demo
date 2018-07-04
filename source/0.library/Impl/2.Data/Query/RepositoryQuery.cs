using library.Impl.Data.Sql;
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

        public RepositoryQuery(ISqlCreator creator, IMapperRepository<T, U> mapper, ISqlBuilderQuery builder)
            : base(creator, mapper)
        {
            _builder = builder;
        }
        public RepositoryQuery(IMapperRepository<T, U> mapper, ConnectionStringSettings connectionstringsettings)
            : this(new SqlCreator(connectionstringsettings), mapper, SqlBuilderQueryFactory.Create(connectionstringsettings))
        {
        }
        public RepositoryQuery(IMapperRepository<T, U> mapper, string appconnectionstringname)
            : this(mapper, ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appconnectionstringname]])
        {
        }

        public virtual (Result result, U data) 
            SelectSingle
            (IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)> querycolumns,
            IList<(IQueryRepositoryProperties internaltable, IList<string> internalalias, IQueryRepositoryProperties externaltable, IList<string> externalalias, IList<(IQueryColumn, IQueryColumn)> joins)> queryjoins,
            string tablename, 
            int maxdepth = 1, 
            U data = default(U))
        {
            var select = _builder.Select(querycolumns, queryjoins, tablename, 1);
            return SelectSingle(select.commandtext, CommandType.Text, select.parameters, maxdepth, data);
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
            var executequery = ExecuteQuery(command, maxdepth, data == null ? default(List<U>) : new List<U> { data });

            return (executequery.result, executequery.datas.FirstOrDefault());
        }

        public virtual (Result result, IEnumerable<U> datas) 
            SelectMultiple
            (IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)> querycolumns,
            IList<(IQueryRepositoryProperties internaltable, IList<string> internalalias, IQueryRepositoryProperties externaltable, IList<string> externalalias, IList<(IQueryColumn, IQueryColumn)> joins)> queryjoins,
            string tablename, 
            int maxdepth = 1, int top = 0, 
            IList<U> datas = null)
        {
            var select = _builder.Select(querycolumns, queryjoins, tablename, top);
            return SelectMultiple(select.commandtext, CommandType.Text, select.parameters, maxdepth, datas);
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
            (IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)> querycolumns,
            IList<(IQueryRepositoryProperties internaltable, IList<string> internalalias, IQueryRepositoryProperties externaltable, IList<string> externalalias, IList<(IQueryColumn, IQueryColumn)> joins)> queryjoins,
            string tablename, 
            IList<ITableColumn> columns)
        {
            var update = _builder.Update(querycolumns, queryjoins, tablename, columns);
            return Update(update.commandtext, CommandType.Text, update.parameters);
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
            (IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)> querycolumns,
            IList<(IQueryRepositoryProperties internaltable, IList<string> internalalias, IQueryRepositoryProperties externaltable, IList<string> externalalias, IList<(IQueryColumn, IQueryColumn)> joins)> queryjoins,
            string tablename)
        {
            var delete = _builder.Delete(querycolumns, queryjoins, tablename);
            return Delete(delete.commandtext, CommandType.Text, delete.parameters);
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
