using library.Impl;
using library.Interface.Data;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Interface.Domain
{
    public interface ILogic<T, U, V> where T : IEntity
                                      where U : IEntityTable<T>
                                      where V : IEntityState<T, U>
    {
        V Clear(V domain, IEntityRepository<T, U> entityrepository);

        (Result result, V domain) Load(V domain, IEntityRepository<T, U> entityrepository, bool usedbcommand = false);
        (Result result, V domain) Save(V domain, IEntityRepository<T, U> entityrepository, bool useinsertdbcommand = false, bool useupdatedbcommand = false);
        (Result result, V domain) Erase(V domain, IEntityRepository<T, U> entityrepository, bool usedbcommand = false);

        (Result result, V domain) Retrieve(IQueryRepository<T, U> queryrepository, int maxdepth = 1, V domains = default(V));
        (Result result, IEnumerable<V> domains) List(IQueryRepository<T, U> queryrepository, int maxdepth = 1, int top = 0, IList<V> domains = null);
    }
}
