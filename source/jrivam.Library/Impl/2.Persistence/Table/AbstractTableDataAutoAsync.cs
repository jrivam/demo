using Autofac;
using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Persistence.Table
{
    public abstract class AbstractTableDataAutoAsync<T, U> : AbstractTableDataAuto<T, U>, ITableDataMethodsAutoAsync<T, U>
        where T : IEntity
        where U : class, ITableData<T, U>
    {
        public AbstractTableDataAutoAsync(IRepositoryTable<T, U> repositorytable,
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

        public virtual async Task<(Result result, U data)> SelectQueryAsync(int? commandtimeout = null,
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

            var selectsingle = await _query.SelectSingleAsync(commandtimeout,
                maxdepth,
                connection);

            return selectsingle;
        }

        public virtual async Task<(Result result, U data)> SelectAsync(int? commandtimeout = null,
            IDbConnection connection = null)
        {
            var select = await _repositorytable.SelectAsync(this as U,
                Helper.CommandTimeout(commandtimeout),
                connection);

            return select;
        }

        public virtual async Task<(Result result, U data)> InsertAsync(int? commandtimeout = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var insert = await _repositorytable.InsertAsync(this as U,
                Helper.CommandTimeout(commandtimeout),
                connection, transaction);

            return insert;
        }

        public virtual async Task<(Result result, U data)> UpdateAsync(int? commandtimeout = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = await _repositorytable.UpdateAsync(this as U,
                Helper.CommandTimeout(commandtimeout),
                connection, transaction);

            return update;
        }

        public virtual async Task<(Result result, U data)> DeleteAsync(int? commandtimeout = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var delete = await _repositorytable.DeleteAsync(this as U,
                Helper.CommandTimeout(commandtimeout),
                connection, transaction);

            return delete;
        }

        public virtual async Task<(Result result, U data)> UpsertAsync(int? commandtimeout = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var select = await SelectAsync(Helper.CommandTimeout(commandtimeout),
                connection);
            if (select.result.Success && select.data != null)
            {
                return await UpdateAsync(Helper.CommandTimeout(commandtimeout),
                    connection, transaction);
            }
            else
            {
                return await InsertAsync(Helper.CommandTimeout(commandtimeout),
                    connection, transaction);
            }
        }
    }
}
