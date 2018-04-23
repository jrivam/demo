using library.Impl;
using library.Interface.Data.Model;
using library.Interface.Data.Query;
using library.Interface.Domain.Model;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Interface.Domain.Query
{
    public interface ILogicQuery<T, U, V> 
        where T : IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>
    {
        (Result result, V domain) Retrieve(IQueryRepository<T, U> queryrepository, int maxdepth = 1, V domains = default(V));
        (Result result, IEnumerable<V> domains) List(IQueryRepository<T, U> queryrepository, int maxdepth = 1, int top = 0, IList<V> domains = null);
    }
}
