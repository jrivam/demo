using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Persistence.Table
{
    public abstract class AbstractTableDataCommandAsync<T, U> : AbstractTableDataCommand<T, U>, ITableDataMethodsCommandAsync<T, U>
        where T : IEntity
        where U : class, ITableData<T, U>
    {
        public AbstractTableDataCommandAsync(IRepositoryTable<T, U> repositorytable,
            IQueryData<T, U> query,
            T entity = default(T),
            string name = null, string dbname = null)
            : base(repositorytable,
                  query,
                  entity,
                  name, dbname)
        {
        }

        public virtual async Task<(Result result, U data)> SelectCommandAsync(
            IDbConnection connection = null)
        {
            if (SelectDbCommand != null)
            {
                return await _repositorytable.SelectAsync(this as U,
                    SelectDbCommand.Value.dbcommand.Text, SelectDbCommand.Value.dbcommand.Type, SelectDbCommand.Value.dbcommand.Timeout,
                    SelectDbCommand.Value.dbcommand.Parameters,
                    connection);
            }

            return (new Result(
                new ResultMessage()
                {
                    Category = ResultCategory.Error,
                    Name = $"{this.GetType().Name}.{nameof(SelectCommandAsync)}",
                    Description = "No SelectDbCommand defined."
                }
                ), default(U));
        }

        public virtual async Task<(Result result, U data)> InsertCommandAsync(
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            if (InsertDbCommand != null)
            {
                return await _repositorytable.InsertAsync(this as U,
                    InsertDbCommand.Value.dbcommand.Text, InsertDbCommand.Value.dbcommand.Type, InsertDbCommand.Value.dbcommand.Timeout,
                    InsertDbCommand.Value.dbcommand.Parameters,
                    connection, transaction);
            }

            return (new Result(
                new ResultMessage()
                {
                    Category = ResultCategory.Error,
                    Name = $"{this.GetType().Name}.{nameof(InsertCommandAsync)}",
                    Description = "No InsertDbCommand defined."
                }
                ), default(U));
        }

        public virtual async Task<(Result result, U data)> UpdateCommandAsync(
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            if (UpdateDbCommand != null)
            {
                return await _repositorytable.UpdateAsync(this as U,
                    UpdateDbCommand.Value.dbcommand.Text, UpdateDbCommand.Value.dbcommand.Type, UpdateDbCommand.Value.dbcommand.Timeout,
                    UpdateDbCommand.Value.dbcommand.Parameters,
                    connection, transaction);
            }

            return (new Result(
                new ResultMessage()
                {
                    Category = ResultCategory.Error,
                    Name = $"{this.GetType().Name}.{nameof(UpdateCommandAsync)}",
                    Description = "No UpdateDbCommand defined."
                }
                ), default(U));
        }

        public virtual async Task<(Result result, U data)> DeleteCommandAsync(
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            if (DeleteDbCommand != null)
            {
                return await _repositorytable.DeleteAsync(this as U,
                    DeleteDbCommand.Value.dbcommand.Text, DeleteDbCommand.Value.dbcommand.Type, DeleteDbCommand.Value.dbcommand.Timeout,
                    DeleteDbCommand.Value.dbcommand.Parameters,
                    connection, transaction);
            }

            return (new Result(
                new ResultMessage()
                {
                    Category = ResultCategory.Error,
                    Name = $"{this.GetType().Name}.{nameof(DeleteCommandAsync)}",
                    Description = "No DeleteDbCommand defined."
                }
                ), default(U));
        }

        public virtual async Task<(Result result, U data)> UpsertCommandAsync(
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var select = await SelectCommandAsync(connection);
            if (select.result.Success && select.data != null)
            {
                return await UpdateCommandAsync(connection, transaction);
            }
            else
            {
                return await InsertCommandAsync(connection, transaction);
            }
        }
    }
}
