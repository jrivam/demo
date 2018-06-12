using library.Impl;
using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Interface.Domain.Query
{
    public interface ILogicQuery<T, U, V> 
        where T : IEntity
        where U : IEntityRepositoryProperties<T>
        where V : IEntityLogicProperties<T, U>
    {
        (Result result, V domain) Retrieve(IQueryRepositoryMethods<T, U> queryrepository, int maxdepth = 1, V domains = default(V));
        (Result result, IEnumerable<V> domains) List(IQueryRepositoryMethods<T, U> queryrepository, int maxdepth = 1, int top = 0, IList<V> domains = null);
    }
}
