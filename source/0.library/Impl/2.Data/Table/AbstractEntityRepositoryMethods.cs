using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Entities;

namespace library.Impl.Data.Table
{
    public abstract class AbstractEntityRepositoryMethods<T, U> : AbstractEntityRepositoryProperties<T>, IEntityRepositoryMethods<T, U>
        where T : IEntity, new()
        where U : class, IEntityRepositoryProperties<T>
    {
        protected readonly IRepositoryTable<T, U> _repository;

        public AbstractEntityRepositoryMethods(IRepositoryTable<T, U> repository,
            string name, string reference)
            : base(name, reference)
        {
            _repository = repository;

            InitDbCommands();
        }
        public AbstractEntityRepositoryMethods(IRepositoryTable<T, U> repository,
            string name, string reference, T entity)
            : this(repository, name, reference)
        {
            SetProperties(entity);
        }

        public virtual U Clear()
        {
            return _repository.Clear(this as U);
        }

        public virtual (Result result, U data) Select(bool usedbcommand = false)
        {
            return _repository.Select(this as U, usedbcommand);
        }
        public abstract (Result result, U data) SelectQuery(int maxdepth = 1, IQueryRepositoryMethods<T, U> query = default(IQueryRepositoryMethods<T, U>));
        public virtual (Result result, U data) Insert(bool usedbcommand = false)
        {
            return _repository.Insert(this as U, usedbcommand);
        }
        public virtual (Result result, U data) Update(bool usedbcommand = false)
        {
            return _repository.Update(this as U, usedbcommand);
        }
        public virtual (Result result, U data) Delete(bool usedbcommand = false)
        {
            return _repository.Delete(this as U, usedbcommand);
        }

        public U SetProperties(T entity)
        {
            return Entities.Helper.SetProperties<T, U>(entity, this as U);
        }
    }
}
