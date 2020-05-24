using Library.Extension;
using Library.Impl.Persistence.Sql;
using Library.Interface.Entities;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Sql;
using Library.Interface.Persistence.Table;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Library.Impl.Persistence.Table
{
    public abstract class AbstractTableData<T, U> : ITableData<T, U>
        where T : IEntity, new()
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

            foreach (var property in this.Entity.GetProperties(isprimitive: true))
            {
                var attributes = typeof(T).GetAttributesFromTypeProperty(property.Name);

                var dbname = ((ColumnAttribute)attributes.FirstOrDefault(x => x.GetType() == typeof(ColumnAttribute)))?.Name ?? property.Name.ToUnderscoreCase().ToLower();
                var iskey = ((KeyAttribute)attributes.FirstOrDefault(x => x.GetType() == typeof(KeyAttribute))) != null;
                var isforeignkey = ((ForeignKeyAttribute)attributes.FirstOrDefault(x => x.GetType() == typeof(ForeignKeyAttribute))) != null;
                var isidentity = ((DatabaseGeneratedAttribute)attributes.FirstOrDefault(x => x.GetType() == typeof(DatabaseGeneratedAttribute)))?.DatabaseGeneratedOption == DatabaseGeneratedOption.Identity;

                var column = (IColumnTable)Activator.CreateInstance(typeof(ColumnTable<>).MakeGenericType(property.PropertyType),
                                    new object[] { this, property.Name, dbname, iskey, isidentity, isforeignkey });
                column.Value = property.GetValue(this.Entity);

                Columns.Add(column);
            }
        }
        protected virtual void InitX()
        {
        }

        protected readonly IRepositoryTable<T, U> _repository;

        protected readonly IQueryData<T, U> _query;

        public AbstractTableData(IRepositoryTable<T, U> repository,
            IQueryData<T, U> query,
            T entity,
            string name, string dbname)
        {
            Description = new Description(name, dbname);

            _repository = repository;

            _query = query;

            Entity = entity;

            Init();
            InitX();
        }

        public virtual (Result result, U data) SelectQuery(int maxdepth = 1)
        {
            _query.Clear();

            var primarykeycolumns = Columns?.Where(x => x.IsPrimaryKey);
            if (primarykeycolumns != null)
            {
                foreach (var primarykeycolumn in primarykeycolumns)
                {
                    _query.Columns[primarykeycolumn.Description.Name].Where(Columns[primarykeycolumn.Description.Name].Value, WhereOperator.Equals);
                }
            }

            var selectsingle = _query.SelectSingle(maxdepth);

            return selectsingle;
        }          

        public virtual (Result result, U data) Select(bool usedbcommand = false)
        {
            var selectsingle = _repository.Select(this as U, usedbcommand);

            return selectsingle;
        }

        public virtual (Result result, U data) Insert(bool usedbcommand = false)
        {
            var insert = _repository.Insert(this as U, usedbcommand);

            return insert;
        }
        public virtual (Result result, U data) Update(bool usedbcommand = false)
        {
            var update = _repository.Update(this as U, usedbcommand);

            return update;
        }
        public virtual (Result result, U data) Delete(bool usedbcommand = false)
        {
            var delete = _repository.Delete(this as U, usedbcommand);

            return delete;
        }
    }
}
