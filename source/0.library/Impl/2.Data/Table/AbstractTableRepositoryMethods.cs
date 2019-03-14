using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Entities;

namespace library.Impl.Data.Table
{
    public abstract class AbstractTableRepositoryMethods<T, U> : AbstractTableRepository<T>, ITableRepositoryMethods<T, U>
        where T : IEntity, new()
        where U : class, ITableRepository, ITableEntity<T>
    {
        protected readonly IRepositoryTable<T, U> _repository;

        public AbstractTableRepositoryMethods(T entity, IRepositoryTable<T, U> repository,
            string name, string reference)
            : base(entity, name, reference)
        {
            _repository = repository;

            Init();
        }

        public virtual (Result result, U data) Select(bool usedbcommand = false)
        {
            var selectsingle = _repository.Select(this as U, usedbcommand);
            this.Entity = selectsingle.data.Entity;
            this.Columns = selectsingle.data.Columns;

            return selectsingle;
        }

        public abstract IQueryRepositoryMethods<T, U> QuerySelect { get; }
        public virtual (Result result, U data) SelectQuery(int maxdepth = 1)
        {
            var selectsingle =  QuerySelect.SelectSingle(maxdepth);
            this.Entity = selectsingle.data.Entity;
            this.Columns = selectsingle.data.Columns;

            return selectsingle;
        }

        public abstract IQueryRepositoryMethods<T, U> QueryUnique { get; }
        public virtual (Result result, U data, bool isunique) CheckIsUnique()
        {
            var selectsingle = QueryUnique.SelectSingle(1);

            return (selectsingle.result, selectsingle.data, selectsingle.data == null);
        }

        public virtual (Result result, U data) Insert(bool usedbcommand = false)
        {
            var insert = _repository.Insert(this as U, usedbcommand);
            this.Columns["Id"].Value = this.Entity.Id = insert.data.Entity.Id;

            Select(usedbcommand);

            return insert;
        }
        public virtual (Result result, U data) Update(bool usedbcommand = false)
        {
            var update = _repository.Update(this as U, usedbcommand);

            Select(usedbcommand);

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
