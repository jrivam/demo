using library.Impl;
using library.Interface.Data.Model;
using library.Interface.Domain.Model;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Interface.Domain.Query
{
    public interface IQueryLogic<T, U, V> 
        where T : IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>
    {
        (Result result, V domain) Retrieve(int maxdepth, V domain = default(V));
        (Result result, IEnumerable<V> domains) List(int maxdepth = 1, int top = 0, IList<V> domains = null);
    }
}
