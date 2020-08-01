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

namespace jrivam.Library.Impl.Persistence.Table
{
    public abstract class AbstractTableData<T, U> : ITableData<T, U>
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

        protected AbstractTableData(IRepositoryTable<T, U> repositorytable,
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

        public virtual (Result result, U data) SelectQuery(int maxdepth = 1, 
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

            var selectsingle = _query.SelectSingle(maxdepth,
                connection);

            return selectsingle;
        }          
        public virtual (Result result, U data) Select(bool usedbcommand = false, 
            IDbConnection connection = null)
        {
            if (Helper.UseDbCommand(UseDbCommand, SelectDbCommand?.usedbcommand ?? false, usedbcommand))
            {
                if (SelectDbCommand != null)
                {
                    return _repositorytable.Select(this as U, SelectDbCommand.Value.dbcommand,
                        connection);
                }

                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Error,
                        Name = nameof(Select),
                        Description = "No SelectDbCommand defined."
                    }
                    ), default(U));
            }

            var select = _repositorytable.Select(this as U,
                connection);

            return select;
        }

        public virtual (Result result, U data) Insert(bool usedbcommand = false, 
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            if (Helper.UseDbCommand(UseDbCommand, InsertDbCommand?.usedbcommand ?? false, usedbcommand))
            {
                if (InsertDbCommand != null)
                {
                    return _repositorytable.Insert(this as U, InsertDbCommand.Value.dbcommand,
                        connection, transaction);
                }

                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Error,
                        Name = nameof(Insert),
                        Description = "No InsertDbCommand defined."
                    }
                    ), default(U));
            }

            var insert = _repositorytable.Insert(this as U,
                connection, transaction);

            return insert;
        }
        public virtual (Result result, U data) Update(bool usedbcommand = false, 
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            if (Helper.UseDbCommand(UseDbCommand, UpdateDbCommand?.usedbcommand ?? false, usedbcommand))
            {
                if (UpdateDbCommand != null)
                {
                    return _repositorytable.Update(this as U, UpdateDbCommand.Value.dbcommand,
                        connection, transaction);
                }

                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Error,
                        Name = nameof(Update),
                        Description = "No UpdateDbCommand defined."
                    }
                    ), default(U));
            }

            var update = _repositorytable.Update(this as U,
                connection, transaction);

            return update;
        }
        public virtual (Result result, U data) Delete(bool usedbcommand = false,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            if (Helper.UseDbCommand(UseDbCommand, DeleteDbCommand?.usedbcommand ?? false, usedbcommand))
            {
                if (DeleteDbCommand != null)
                {
                    return _repositorytable.Delete(this as U, DeleteDbCommand.Value.dbcommand,
                        connection, transaction);
                }

                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Error,
                        Name = nameof(Delete),
                        Description = "No DeleteDbCommand defined."
                    }
                    ), default(U));
            }

            var delete = _repositorytable.Delete(this as U, 
                connection, transaction);

            return delete;
        }

        public virtual (Result result, U data) Upsert(bool usedbcommand = false, 
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var select = Select(usedbcommand,
                connection);
            if (select.result.Success && select.data != null)
            {
                return Update(usedbcommand,
                    connection, transaction);
            }
            else
            {
                return Insert(usedbcommand,
                    connection, transaction);
            }
        }
    }
}
