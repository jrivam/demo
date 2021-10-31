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
    public abstract partial class AbstractTableData<T, U> : ITableData<T, U>
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

        public virtual Description Description { get; protected set; }

        public virtual IColumnTable this[string name]
        {
            get
            {
                return Columns[name];
            }
        }
        public virtual ListColumns<IColumnTable> Columns { get; set; } = new ListColumns<IColumnTable>();

        protected readonly IRepositoryTableAsync<T, U> _repositorytableasync;
        protected readonly IQueryData<T, U> _queryasync;

        public virtual (bool usedbcommand, ISqlCommand dbcommand)? SelectDbCommand { get; set; }
        public virtual (bool usedbcommand, ISqlCommand dbcommand)? InsertDbCommand { get; set; }
        public virtual (bool usedbcommand, ISqlCommand dbcommand)? UpdateDbCommand { get; set; }
        public virtual (bool usedbcommand, ISqlCommand dbcommand)? DeleteDbCommand { get; set; }

        public AbstractTableData(IRepositoryTableAsync<T, U> repositorytableasync,
            IQueryData<T, U> queryasync,
            T entity = default(T),
            string name = null, string dbname = null)
        {
            if (entity == null)
                _entity = HelperEntities<T>.CreateEntity();
            else
                _entity = entity;

            Description = new Description(name ?? typeof(T).Name, dbname ?? typeof(T).GetAttributeFromType<TableAttribute>()?.Name ?? typeof(T).Name);

            Init();

            _repositorytableasync = repositorytableasync;
            _queryasync = queryasync;
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

        public virtual async Task<(Result result, U data)> SelectQueryAsync(int maxdepth = 1,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            _queryasync.ClearConditions();

            var primarykeycolumns = Columns?.Where(x => x.IsPrimaryKey);
            if (primarykeycolumns != null)
            {
                foreach (var primarykeycolumn in primarykeycolumns)
                {
                    _queryasync[primarykeycolumn.Description.Name].Where(Columns[primarykeycolumn.Description.Name].Value, WhereOperator.Equals);
                }
            }

            var selectsingle = await _queryasync.SelectSingleAsync(maxdepth,
                connection,
                commandtimeout).ConfigureAwait(false);

            return selectsingle;
        }

        public virtual async Task<(Result result, U data)> SelectAsync(
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            if (SelectDbCommand != null)
            {
                return await _repositorytableasync.SelectAsync(this as U,
                    SelectDbCommand.Value.dbcommand.Text, SelectDbCommand.Value.dbcommand.Type,
                    SelectDbCommand.Value.dbcommand.Parameters,
                    connection,
                    Helper.CommandTimeout(commandtimeout)).ConfigureAwait(false);
            }

            var select = await _repositorytableasync.SelectAsync(this as U,
                connection,
                Helper.CommandTimeout(commandtimeout)).ConfigureAwait(false);

            return select;
        }

        public virtual async Task<(Result result, U data)> InsertAsync(
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            if (InsertDbCommand != null)
            {
                return await _repositorytableasync.InsertAsync(this as U,
                    InsertDbCommand.Value.dbcommand.Text, InsertDbCommand.Value.dbcommand.Type,
                    InsertDbCommand.Value.dbcommand.Parameters,
                    connection, transaction,
                    Helper.CommandTimeout(commandtimeout)).ConfigureAwait(false);
            }

            var insert = await _repositorytableasync.InsertAsync(this as U,
                connection, transaction,
                Helper.CommandTimeout(commandtimeout)).ConfigureAwait(false);

            return insert;
        }

        public virtual async Task<(Result result, U data)> UpdateAsync(
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            if (UpdateDbCommand != null)
            {
                return await _repositorytableasync.UpdateAsync(this as U,
                    UpdateDbCommand.Value.dbcommand.Text, UpdateDbCommand.Value.dbcommand.Type,
                    UpdateDbCommand.Value.dbcommand.Parameters,
                    connection, transaction,
                    Helper.CommandTimeout(commandtimeout)).ConfigureAwait(false);
            }

            var update = await _repositorytableasync.UpdateAsync(this as U,
                connection, transaction,
                Helper.CommandTimeout(commandtimeout)).ConfigureAwait(false);

            return update;
        }

        public virtual async Task<(Result result, U data)> DeleteAsync(
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            if (DeleteDbCommand != null)
            {
                return await _repositorytableasync.DeleteAsync(this as U,
                    DeleteDbCommand.Value.dbcommand.Text, DeleteDbCommand.Value.dbcommand.Type,
                    DeleteDbCommand.Value.dbcommand.Parameters,
                    connection, transaction,
                    Helper.CommandTimeout(commandtimeout)).ConfigureAwait(false);
            }

            var delete = await _repositorytableasync.DeleteAsync(this as U,
                connection, transaction,
                Helper.CommandTimeout(commandtimeout)).ConfigureAwait(false);

            return delete;
        }

        public virtual async Task<(Result result, U data)> UpsertAsync(
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            var select = await SelectAsync(connection,
                commandtimeout).ConfigureAwait(false);
            if (select.result.Success)
            {
                if (select.data != null)
                {
                    return await UpdateAsync(connection, transaction,
                        commandtimeout).ConfigureAwait(false);
                }
                else
                {
                    return await InsertAsync(connection, transaction,
                        commandtimeout).ConfigureAwait(false);
                }
            }

            return (select.result, default(U));
        }
    }
}
