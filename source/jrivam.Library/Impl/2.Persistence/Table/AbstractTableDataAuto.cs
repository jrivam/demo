using Autofac;
using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;
using System.Data;
using System.Linq;

namespace jrivam.Library.Impl.Persistence.Table
{
    public abstract class AbstractTableDataAuto<T, U> : AbstractTableDataBase<T, U>, ITableDataMethodsAuto<T, U>
        where T : IEntity
        where U : class, ITableData<T, U>
    {
        public AbstractTableDataAuto(IRepositoryTable<T, U> repositorytable,
            IQueryData<T, U> query,
            T entity = default(T),
            string name = null,
            string dbname = null)
            : base(repositorytable,
                  query,
                  entity,
                  name, dbname)
        {
        }

        public virtual (Result result, U data) SelectQuery(int? commandtimeout = null, 
            int maxdepth = 1, 
            IDbConnection connection = null)
        {
            _query.ClearConditions();

            var primarykeycolumns = Columns?.Where(x => x.IsPrimaryKey);
            if (primarykeycolumns != null)
            {
                foreach (var primarykeycolumn in primarykeycolumns)
                {
                    _query[primarykeycolumn.Description.Name].Where(Columns[primarykeycolumn.Description.Name].Value, WhereOperator.Equals);
                }
            }

            var selectsingle = _query.SelectSingle(commandtimeout,
                maxdepth,
                connection);

            return selectsingle;
        }

        public virtual (Result result, U data) Select(int? commandtimeout = null,
            IDbConnection connection = null)
        {
            var select = _repositorytable.Select(this as U,
                Helper.CommandTimeout(commandtimeout),
                connection);

            return select;
        }

        public virtual (Result result, U data) Insert(int? commandtimeout = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var insert = _repositorytable.Insert(this as U,
                Helper.CommandTimeout(commandtimeout),
                connection, transaction);

            return insert;
        }

        public virtual (Result result, U data) Update(int? commandtimeout = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = _repositorytable.Update(this as U,
                Helper.CommandTimeout(commandtimeout),
                connection, transaction);

            return update;
        }

        public virtual (Result result, U data) Delete(int? commandtimeout = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var delete = _repositorytable.Delete(this as U,
                Helper.CommandTimeout(commandtimeout),
                connection, transaction);

            return delete;
        }

        public virtual (Result result, U data) Upsert(int? commandtimeout = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var select = Select(Helper.CommandTimeout(commandtimeout),
                connection);
            if (select.result.Success && select.data != null)
            {
                return Update(Helper.CommandTimeout(commandtimeout),
                    connection, transaction);
            }
            else
            {
                return Insert(Helper.CommandTimeout(commandtimeout), 
                    connection, transaction);
            }
        }
    }
}
