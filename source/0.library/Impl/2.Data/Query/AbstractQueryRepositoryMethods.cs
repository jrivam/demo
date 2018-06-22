using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Impl.Data.Query
{
    public abstract class AbstractQueryRepositoryMethods<T, U> : AbstractQueryRepositoryProperties, IQueryRepositoryMethods<T, U>
        where T : IEntity, new()
        where U : class, ITableRepositoryProperties<T>
    {
        protected readonly IRepositoryQuery<T, U> _repository;

        public AbstractQueryRepositoryMethods(IRepositoryQuery<T, U> repository,
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

        public virtual (Result result, int rows) Update(IList<ITableColumn> columns, int maxdepth = 1)
        {
            return _repository.Update(columns, this, maxdepth);
        }
        public virtual (Result result, int rows) Delete(int maxdepth = 1)
        {
            return _repository.Delete(this, maxdepth);
        }
    }
}
