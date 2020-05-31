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

        protected readonly IRepositoryTable<T, U> _repositorytable;
        protected readonly IRepositoryQuery<T, U> _repositoryquery;

        protected AbstractTableData(
            IRepositoryTable<T, U> repositorytable,
            IRepositoryQuery<T, U> repositoryquery, 
            T entity = default(T),
            string name = null, 
            string dbname = null)
        {
            _repositorytable = repositorytable;
            _repositoryquery = repositoryquery;

            if (entity == null)
                Entity = HelperEntities<T>.CreateEntity();
            else
                Entity = entity;

            Description = new Description(name ?? typeof(T).Name, dbname ?? typeof(T).GetAttributeFromType<TableAttribute>()?.Name ?? typeof(T).Name);

            Init();
        }

        public virtual (Result result, U data) SelectQuery(int maxdepth = 1)
        {
            var query = (IQueryData<T, U>)Activator.CreateInstance(typeof(IQueryData<T, U>),
                                new object[] {  });
            //var query = (IQueryData<T, U>)Activator.CreateInstance(typeof(IQueryData<T, U>),
            //        BindingFlags.CreateInstance |
            //        BindingFlags.Public |
            //        BindingFlags.Instance |
            //        BindingFlags.OptionalParamBinding,
            //        null, new object[] { _repositoryquery, null, null },
            //        CultureInfo.CurrentCulture);

            query.Clear();

            var primarykeycolumns = Columns?.Where(x => x.IsPrimaryKey);
            if (primarykeycolumns != null)
            {
                foreach (var primarykeycolumn in primarykeycolumns)
                {
                    query.Columns[primarykeycolumn.Description.Name].Where(Columns[primarykeycolumn.Description.Name].Value, WhereOperator.Equals);
                }
            }

            var selectsingle = query.SelectSingle(maxdepth);

            return selectsingle;
        }          

        public virtual (Result result, U data) Select(bool usedbcommand = false)
        {
            var select = _repositorytable.Select(this as U, usedbcommand);

            return select;
        }

        public virtual (Result result, U data) Insert(bool usedbcommand = false)
        {
            var insert = _repositorytable.Insert(this as U, usedbcommand);

            return insert;
        }
        public virtual (Result result, U data) Update(bool usedbcommand = false)
        {
            var update = _repositorytable.Update(this as U, usedbcommand);

            return update;
        }
        public virtual (Result result, U data) Delete(bool usedbcommand = false)
        {
            var delete = _repositorytable.Delete(this as U, usedbcommand);

            return delete;
        }

        public virtual void Clear(U data)
        {
            foreach (var column in data.Columns)
            {
                data[column.Description.Name].DbValue = null;
            }
        }
        public virtual void Map(U data, int maxdepth = 1, int depth = 0)
        {
            foreach (var property in typeof(U).GetPropertiesFromType(isprimitive: true, isforeign: true, attributetypes: new System.Type[] { typeof(DataAttribute) }))
            {
                if (property.isprimitive)
                {
                    var entityproperty = typeof(T).GetPropertyFromType(property.info.Name);
                    data[property.info.Name].DbValue = entityproperty.GetValue(this.Entity);
                }

                if (property.isforeign)
                {
                    depth++;
                    if (depth < maxdepth || maxdepth == 0)
                    {
                        var foreign = property.info.GetValue(this);
                        if (foreign != null)
                        {
                            foreign.GetType().GetMethod(nameof(Map)).Invoke(foreign, new object[] { foreign, maxdepth, depth });
                        }
                    }
                }
            }
        }
    }
}
