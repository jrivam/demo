using library.Impl;
using library.Interface.Data;
using library.Interface.Domain;
using System.Collections.Generic;

namespace library.Interface.Business
{
    public interface ILogic<T, U, V> where T : IEntity
                                      where U : IEntityTable<T>
                                      where V : IEntityState<T, U>
    {
        V Clear(V business, IEntityRepository<T, U> repository, int maxdepth = 1);

        (Result result, V business) Load(V business, IEntityRepository<T, U> repository);
        (Result result, V business) Save(V business, IEntityRepository<T, U> repository);
        (Result result, V business) Erase(V business, IEntityRepository<T, U> repository);

        (Result result, V business) Retrieve(IQueryRepository<T, U> repository, int maxdepth = 1, V business = default(V));
        (Result result, IEnumerable<V> businesses) List(IQueryRepository<T, U> repository, int maxdepth =1, int top = 0, IList<V> businesses = null);
    }
}
