using library.Interface.Data;
using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Impl.Data.Query
{
    public abstract class AbstractQueryRepositoryMethods<S, T, U> : AbstractQueryRepository, IQueryRepositoryMethods<T, U>
        where T : IEntity, new()
        where U : class, ITableRepository, ITableEntity<T>
        where S : IQueryRepositoryMethods<T, U>
    {
        protected readonly IRepositoryQuery<S, T, U> _repository;

        public AbstractQueryRepositoryMethods(IRepositoryQuery<S, T, U> repository,
            string name, string reference)
            : base(name, reference)
        {
            _repository = repository;
        }

        public virtual (Result result, U data) SelectSingle(int maxdepth = 1, U data = default(U))
        {
            return _repository.SelectSingle(this, maxdepth, data);
        }
        public virtual (Result result, IEnumerable<U> datas) SelectMultiple(int maxdepth = 1, int top = 0, IListData<T, U> datas = null)
        {
            return _repository.SelectMultiple(this, maxdepth, top, datas);
        }

        public virtual (Result result, int rows) Update(IList<(ITableRepository table, ITableColumn column)> tablecolumns, int maxdepth = 1)
        {
            return _repository.Update(this, tablecolumns, maxdepth);
        }
        public virtual (Result result, int rows) Delete(int maxdepth = 1)
        {
            return _repository.Delete(this, maxdepth);
        }
    }
}
