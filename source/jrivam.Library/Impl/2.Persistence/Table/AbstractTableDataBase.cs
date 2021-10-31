using Autofac;
using jrivam.Library.Extension;
using jrivam.Library.Impl.Entities;
using jrivam.Library.Impl.Persistence.Attributes;
using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Sql;
using jrivam.Library.Interface.Persistence.Table;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Persistence.Table
{
    public abstract class AbstractTableDataBase<T, U> : ITableData<T, U>
        where T : IEntity
        where U : class, ITableData<T, U>
    {
        protected T _entity;
        public virtual T Entity
        {
            get
            {
                return _entity;
            }
            set
            {
                _entity = value;

                foreach (var column in Columns)
                {
                    Columns[column.Description.Name].Value = typeof(T).GetPropertyFromType(column.Description.Name).GetValue(_entity);
                }
            }
        }

        protected IQueryData<T, U> _query;
        public virtual IQueryData<T, U> Query
        { 
            get
            {
                return _query;
            }
            set 
            {
                _query = value;
            }
        }

        public virtual Description Description { get; protected set; }

        public virtual IColumnTable this[string name]
        {
            get
            {
                return Columns[name];
            }
        }
        public virtual ListColumns<IColumnTable> Columns { get; set; } = new ListColumns<IColumnTable>();

        public virtual bool UseDbCommand { get; set; }
        public virtual (bool usedbcommand, ISqlCommand dbcommand)? SelectDbCommand { get; set; }
        public virtual (bool usedbcommand, ISqlCommand dbcommand)? InsertDbCommand { get; set; }
        public virtual (bool usedbcommand, ISqlCommand dbcommand)? UpdateDbCommand { get; set; }
        public virtual (bool usedbcommand, ISqlCommand dbcommand)? DeleteDbCommand { get; set; }

        protected readonly IRepositoryTable<T, U> _repositorytable;

        protected AbstractTableDataBase(IRepositoryTable<T, U> repositorytable,
            IQueryData<T, U> query,
            T entity = default(T),
            string name = null, 
            string dbname = null)
        {
            _repositorytable = repositorytable;

            _query = query;

            if (entity == null)
                _entity = HelperEntities<T>.CreateEntity();
            else
                _entity = entity;

            Description = new Description(name ?? typeof(T).Name, dbname ?? typeof(T).GetAttributeFromType<TableAttribute>()?.Name ?? typeof(T).Name);

            Init();
        }

        protected virtual void Init()
        {
            Columns.Clear();

            foreach (var property in typeof(U).GetPropertiesFromType(isprimitive: true, attributetypes: new System.Type[] { typeof(DataAttribute) }))
            {
                var attributes = typeof(T).GetAttributesFromProperty(property.info.Name);

                var dbname = ((ColumnAttribute)attributes.FirstOrDefault(x => x.GetType() == typeof(ColumnAttribute)))?.Name ?? property.info.Name;
                var iskey = ((KeyAttribute)attributes.FirstOrDefault(x => x.GetType() == typeof(KeyAttribute))) != null;
                var isforeignkey = ((ForeignKeyAttribute)attributes.FirstOrDefault(x => x.GetType() == typeof(ForeignKeyAttribute))) != null;
                var isidentity = ((DatabaseGeneratedAttribute)attributes.FirstOrDefault(x => x.GetType() == typeof(DatabaseGeneratedAttribute)))?.DatabaseGeneratedOption == DatabaseGeneratedOption.Identity;

                var column = (IColumnTable)Activator.CreateInstance(typeof(ColumnTable<>).MakeGenericType(property.info.PropertyType),
                                    new object[] { this, property.info.Name, dbname, iskey, isidentity, isforeignkey });

                var entityproperty = typeof(T).GetPropertyFromType(property.info.Name);
                column.Value = entityproperty.GetValue(this.Entity);

                Columns.Add(column);
            }
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

        public virtual (Result result, U data) Select(int? commandtimeout = null,
            IDbConnection connection = null)
        {
            var select = _repositorytable.Select(this as U,
                Helper.CommandTimeout(commandtimeout),
                connection);

            return select;
        }
        public virtual async Task<(Result result, U data)> SelectAsync(int? commandtimeout = null,
            IDbConnection connection = null)
        {
            var select = await _repositorytable.SelectAsync(this as U,
                Helper.CommandTimeout(commandtimeout),
                connection);

            return select;
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
                    Name = $"{this.GetType().Name}.{nameof(Select)}",
                    Description = "No SelectDbCommand defined."
                }
                ), default(U));
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
                    Name = $"{this.GetType().Name}.{nameof(Select)}",
                    Description = "No SelectDbCommand defined."
                }
                ), default(U));
        }

        public virtual (Result result, U data) Insert(int? commandtimeout = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var insert = _repositorytable.Insert(this as U,
                Helper.CommandTimeout(commandtimeout),
                connection, transaction);

            return insert;
        }
        public virtual async Task<(Result result, U data)> InsertAsync(int? commandtimeout = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var insert = await _repositorytable.InsertAsync(this as U,
                Helper.CommandTimeout(commandtimeout),
                connection, transaction);

            return insert;
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
                    Name = $"{this.GetType().Name}.{nameof(Insert)}",
                    Description = "No InsertDbCommand defined."
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
                    Name = $"{this.GetType().Name}.{nameof(Insert)}",
                    Description = "No InsertDbCommand defined."
                }
                ), default(U));
        }

        public virtual (Result result, U data) Update(int? commandtimeout = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = _repositorytable.Update(this as U,
                Helper.CommandTimeout(commandtimeout),
                connection, transaction);

            return update;
        }
        public virtual async Task<(Result result, U data)> UpdateAsync(int? commandtimeout = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = await _repositorytable.UpdateAsync(this as U,
                Helper.CommandTimeout(commandtimeout),
                connection, transaction);

            return update;
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
                    Name = $"{this.GetType().Name}.{nameof(Update)}",
                    Description = "No UpdateDbCommand defined."
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
                    Name = $"{this.GetType().Name}.{nameof(Update)}",
                    Description = "No UpdateDbCommand defined."
                }
                ), default(U));
        }

        public virtual (Result result, U data) Delete(int? commandtimeout = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var delete = _repositorytable.Delete(this as U,
                Helper.CommandTimeout(commandtimeout),
                connection, transaction);

            return delete;
        }
        public virtual async Task<(Result result, U data)> DeleteAsync(int? commandtimeout = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var delete = await _repositorytable.DeleteAsync(this as U,
                Helper.CommandTimeout(commandtimeout),
                connection, transaction);

            return delete;
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
                    Name = $"{this.GetType().Name}.{nameof(Delete)}",
                    Description = "No DeleteDbCommand defined."
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
                    Name = $"{this.GetType().Name}.{nameof(Delete)}",
                    Description = "No DeleteDbCommand defined."
                }
                ), default(U));
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
