using library.Impl.Entities;
using library.Interface.Data.Model;
using library.Interface.Entities;

namespace library.Impl.Data.Model
{
    public abstract class AbstractEntityRepository<T, U> : AbstractEntityTable<T>, IEntityRepository<T, U>
        where T : IEntity, new()
        where U : class, IEntityTable<T>
    {
        public IRepositoryTable<T, U> _repository { get; protected set; }

        public AbstractEntityRepository(IRepositoryTable<T, U> repository,
            string name, string reference)
            : base(name, reference)
        {
            _repository = repository;
        }

        public virtual U Clear()
        {
            return _repository.Clear(this as U);
        }

        public virtual (Result result, U data) Select(bool usedbcommand = false)
        {
            var select = _repository.Select(this as U, usedbcommand);

            return select;
        }
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

        public void SetProperties(T entity)
        {
            Helper.SetProperties<T, U>(entity, this as U);
        }
    }
}
