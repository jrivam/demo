using library.Impl;
using library.Interface.Data;
using library.Interface.Domain;
using System.Collections.Generic;

namespace library.Interface.Business
{
    public interface IQueryLogic<T, U, V> where T : IEntity
                                            where U : IEntityTable<T>
                                            where V : IEntityState<T, U>
    {
        (Result result, V business) Retrieve(int maxdepth, V business = default(V));
        (Result result, IEnumerable<V> businesses) List(int maxdepth, int top);
    }
}
