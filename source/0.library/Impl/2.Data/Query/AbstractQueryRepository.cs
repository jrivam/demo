using library.Interface.Data.Model;
using library.Interface.Data.Query;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Impl.Data.Query
{
    public abstract class AbstractQueryRepository<T, U> : AbstractQueryTable, IQueryRepository<T, U>
        where T : IEntity, new()
        where U : class, IEntityTable<T>
    {
        public IRepositoryQuery<T, U> _repository { get; protected set; }

        public AbstractQueryRepository(IRepositoryQuery<T, U> repository,
            string name, string reference)
            : base(name, reference)
        {
            _repository = repository;
        }

        public virtual (Result result, U data) SelectSingle(int maxdepth = 1, U data = default(U))
        {
            return _repository.SelectSingle(this, maxdepth, data);
        }
        public virtual (Result result, IEnumerable<U> datas) SelectMultiple(int maxdepth = 1, int top = 0, IList<U> datas = null)
        {
            return _repository.SelectMultiple(this, maxdepth, top, datas);
        }

        public virtual (Result result, int rows) Update(U entity, int maxdepth = 1)
        {
            return _repository.Update(entity, this, maxdepth);
        }
        public virtual (Result result, int rows) Delete(int maxdepth = 1)
        {
            return _repository.Delete(this, maxdepth);
        }
    }
}
