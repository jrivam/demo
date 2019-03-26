using Library.Impl;
using Library.Interface.Data.Table;
using Library.Interface.Domain.Table;
using Library.Interface.Entities;
using System.Collections.Generic;

namespace Library.Interface.Domain.Query
{
    public interface IQueryDomainMethods<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        (Result result, V domain) Retrieve(int maxdept, V domain = default(V));
        (Result result, IEnumerable<V> domains) List(int maxdepth = 1, int top = 0, IList<V> domains = null);
    }
}
