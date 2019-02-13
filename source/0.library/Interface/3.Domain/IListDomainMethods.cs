using library.Impl;
using library.Impl.Domain;
using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Domain.Query;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Interface.Domain
{
    public interface IListDomainMethods<S, R, T, U, V> : IList<V>
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where V : ITableLogic<T, U>, ITableLogicMethods<T, U, V>
        where S : IQueryRepositoryMethods<T, U>
        where R : IQueryLogicMethods<T, U, V>
    {
        (Result result, ListDomain<S, R, T, U, V> list) Load(R query, int maxdepth = 1, int top = 0);
        ListDomain<S, R, T, U, V> Load(IEnumerable<V> list);

        Result SaveAll();
        Result EraseAll();
    }
}
