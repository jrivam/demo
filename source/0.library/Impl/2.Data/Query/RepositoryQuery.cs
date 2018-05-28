using library.Impl.Data.Sql;
using library.Impl.Data.Sql.Builder;
using library.Interface.Data.Mapper;
using library.Interface.Data.Model;
using library.Interface.Data.Query;
using library.Interface.Data.Sql;
using library.Interface.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace library.Impl.Data.Repository
{
    public class RepositoryQuery<T, U> : Repository<T, U>, IRepositoryQuery<T, U> 
        where T : IEntity
        where U : IEntityTable<T>
    {
        public RepositoryQuery(IMapperTable<T, U> mapper, ISqlBuilder<T> builder)
            : base(mapper, builder)
        {
        }
        public RepositoryQuery(IMapperTable<T, U> mapper, string connectionstringname)
            : this(mapper, SqlBuilderFactory<T>.GetBuilder(connectionstringname))
        {
        }

        public virtual (Result result, U data) SelectSingle(IQueryTable querytable, int maxdepth = 1, U data = default(U))
        {
            return SelectSingle(_builder.Select(querytable, maxdepth, 1), maxdepth, data);
        }
        public virtual (Result result, U data) SelectSingle(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null, int maxdepth = 1, U data = default(U))
        {
            return SelectSingle(_builder.GetCommand(commandtext, commandtype, parameters), maxdepth, data);
        }
        public virtual (Result result, U data) SelectSingle(IDbCommand command, int maxdepth = 1, U data = default(U))
        {
            var executequery = ExecuteQuery(command, maxdepth, new List<U> { data });

            return (executequery.result, executequery.datas.FirstOrDefault());
        }

        public virtual (Result result, IEnumerable<U> datas) SelectMultiple(IQueryTable querytable, int maxdepth = 1, int top = 0, IList<U> datas = null)
        {
            return SelectMultiple(_builder.Select(querytable, maxdepth, top), maxdepth, datas);
        }
        public virtual (Result result, IEnumerable<U> datas) SelectMultiple(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null, int maxdepth = 1, IList<U> datas = null)
        {
            return SelectMultiple(_builder.GetCommand(commandtext, commandtype, parameters), maxdepth, datas);
        }
        public virtual (Result result, IEnumerable<U> datas) SelectMultiple(IDbCommand command, int maxdepth = 1, IList<U> datas = null)
        {
            return ExecuteQuery(command, maxdepth, datas);
        }

        public virtual (Result result, int rows) Update(U table, IQueryTable querytable, int maxdepth = 1)
        {
            return Update(_builder.Update(table, querytable, maxdepth));
        }
        public virtual (Result result, int rows) Update(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null)
        {
            return Update(_builder.GetCommand(commandtext, commandtype, parameters));
        }
        public virtual (Result result, int rows) Update(IDbCommand command)
        {
            return ExecuteNonQuery(command);
        }

        public virtual (Result result, int rows) Delete(IQueryTable querytable, int maxdepth = 1)
        {
            return Delete(_builder.Delete(querytable, maxdepth));
        }
        public virtual (Result result, int rows) Delete(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null)
        {
            return Delete(_builder.GetCommand(commandtext, commandtype, parameters));
        }
        public virtual (Result result, int rows) Delete(IDbCommand command)
        {
            return ExecuteNonQuery(command);
        }
    }
}
