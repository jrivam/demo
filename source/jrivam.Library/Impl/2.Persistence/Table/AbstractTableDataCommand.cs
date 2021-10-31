using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Sql;
using jrivam.Library.Interface.Persistence.Table;
using System.Data;

namespace jrivam.Library.Impl.Persistence.Table
{
    public abstract class AbstractTableDataCommand<T, U> : AbstractTableDataBase<T, U>, ITableDataMethodsCommand<T, U>, ITableDataCommands
        where T : IEntity
        where U : class, ITableData<T, U>
    {
        public virtual bool UseDbCommand { get; set; }
        public virtual (bool usedbcommand, ISqlCommand dbcommand)? SelectDbCommand { get; set; }
        public virtual (bool usedbcommand, ISqlCommand dbcommand)? InsertDbCommand { get; set; }
        public virtual (bool usedbcommand, ISqlCommand dbcommand)? UpdateDbCommand { get; set; }
        public virtual (bool usedbcommand, ISqlCommand dbcommand)? DeleteDbCommand { get; set; }

        public AbstractTableDataCommand(IRepositoryTable<T, U> repositorytable,
            IQueryData<T, U> query,
            T entity = default(T),
            string name = null, string dbname = null)
            : base(repositorytable,
                  query,
                  entity,
                  name, dbname)
        {
        }

        public virtual (Result result, U data) SelectCommand(
            IDbConnection connection = null)
        {
            if (SelectDbCommand != null)
            {
                return _repositorytable.Select(this as U,
                    SelectDbCommand.Value.dbcommand.Text, SelectDbCommand.Value.dbcommand.Type, SelectDbCommand.Value.dbcommand.Timeout,
                    SelectDbCommand.Value.dbcommand.Parameters,
                    connection);
            }

            return (new Result(
                new ResultMessage()
                {
                    Category = ResultCategory.Error,
                    Name = $"{this.GetType().Name}.{nameof(SelectCommand)}",
                    Description = "No SelectDbCommand defined."
                }
                ), default(U));
        }

        public virtual (Result result, U data) InsertCommand(
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            if (InsertDbCommand != null)
            {
                return _repositorytable.Insert(this as U,
                    InsertDbCommand.Value.dbcommand.Text, InsertDbCommand.Value.dbcommand.Type, InsertDbCommand.Value.dbcommand.Timeout,
                    InsertDbCommand.Value.dbcommand.Parameters,
                    connection, transaction);
            }

            return (new Result(
                new ResultMessage()
                {
                    Category = ResultCategory.Error,
                    Name = $"{this.GetType().Name}.{nameof(InsertCommand)}",
                    Description = "No InsertDbCommand defined."
                }
                ), default(U));
        }

        public virtual (Result result, U data) UpdateCommand(
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            if (UpdateDbCommand != null)
            {
                return _repositorytable.Update(this as U,
                    UpdateDbCommand.Value.dbcommand.Text, UpdateDbCommand.Value.dbcommand.Type, UpdateDbCommand.Value.dbcommand.Timeout,
                    UpdateDbCommand.Value.dbcommand.Parameters,
                    connection, transaction);
            }

            return (new Result(
                new ResultMessage()
                {
                    Category = ResultCategory.Error,
                    Name = $"{this.GetType().Name}.{nameof(UpdateCommand)}",
                    Description = "No UpdateDbCommand defined."
                }
                ), default(U));
        }

        public virtual (Result result, U data) DeleteCommand(
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            if (DeleteDbCommand != null)
            {
                return _repositorytable.Delete(this as U,
                    DeleteDbCommand.Value.dbcommand.Text, DeleteDbCommand.Value.dbcommand.Type, DeleteDbCommand.Value.dbcommand.Timeout,
                    DeleteDbCommand.Value.dbcommand.Parameters,
                    connection, transaction);
            }

            return (new Result(
                new ResultMessage()
                {
                    Category = ResultCategory.Error,
                    Name = $"{this.GetType().Name}.{nameof(DeleteCommand)}",
                    Description = "No DeleteDbCommand defined."
                }
                ), default(U));
        }

        public virtual (Result result, U data) UpsertCommand(
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var select = SelectCommand(connection);
            if (select.result.Success && select.data != null)
            {
                return UpdateCommand(connection, transaction);
            }
            else
            {
                return InsertCommand(connection, transaction);
            }
        }
    }
}
