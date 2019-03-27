using Library.Impl.Persistence.Sql;
using Library.Interface.Entities;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Data;

namespace Library.Impl.Persistence.Table
{
    public abstract class AbstractTableData<T, U> : ITableData<T, U>
        where T : IEntity, new()
        where U : class, ITableData<T, U>
    {
        public virtual T Entity { get; set; }

        public virtual Description Description { get; protected set; }

        public virtual IColumnTable this[string reference]
        {
            get
            {
                return Columns[reference];
            }
        }
        public virtual ListColumns<IColumnTable> Columns { get; set; } = new ListColumns<IColumnTable>();

        public virtual bool UseDbCommand { get; set; }
        public virtual (bool usedbcommand, (string text, CommandType type, IList<SqlParameter> parameters) dbcommand)? SelectDbCommand { get; set; }
        public virtual (bool usedbcommand, (string text, CommandType type, IList<SqlParameter> parameters) dbcommand)? InsertDbCommand { get; set; }
        public virtual (bool usedbcommand, (string text, CommandType type, IList<SqlParameter> parameters) dbcommand)? UpdateDbCommand { get; set; }
        public virtual (bool usedbcommand, (string text, CommandType type, IList<SqlParameter> parameters) dbcommand)? DeleteDbCommand { get; set; }

        public virtual void Init()
        {
        }

        protected readonly IRepositoryTable<T, U> _repository;

        public AbstractTableData(T entity, IRepositoryTable<T, U> repository,
            string name, string reference)
        {
            Description = new Description(name, reference);

            Entity = entity;

            _repository = repository;

            Init();
        }

        public virtual (Result result, U data) Select(bool usedbcommand = false)
        {
            var selectsingle = _repository.Select(this as U, usedbcommand);

            return selectsingle;
        }

        public abstract IQueryData<T, U> QuerySelect { get; }
        public virtual (Result result, U data) SelectQuery(int maxdepth = 1)
        {
            var selectsingle =  QuerySelect.SelectSingle(maxdepth);

            return selectsingle;
        }

        public abstract IQueryData<T, U> QueryUnique { get; }
        public virtual (Result result, U data, bool isunique) CheckIsUnique()
        {
            var selectsingle = QueryUnique.SelectSingle(1);

            return (selectsingle.result, selectsingle.data, selectsingle.data == null);
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

        public U SetProperties(T entity, bool nulls = false)
        {
            return Entities.Helper.SetProperties<T, U>(entity, this as U, nulls);
        }
    }
}
