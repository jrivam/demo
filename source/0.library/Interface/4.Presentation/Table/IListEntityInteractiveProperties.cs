using library.Impl;
using library.Impl.Domain.Table;
using library.Interface.Data.Query;
using library.Interface.Domain.Query;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation.Query;
using library.Interface.Presentation.Table;
using System.Collections.Generic;

namespace library.Interface.Data.Table
{
    public interface IListEntityInteractiveProperties<S, R, Q, T, U, V, W>
        where S : IQueryRepositoryMethods<T, U>
        where R : IQueryLogicMethods<T, U, V>
        where Q : IQueryInteractiveMethods<T, U, V, W>
        where T : IEntity
        where U : ITableRepositoryProperties<T>
        where V : IEntityLogicProperties<T, U>, IEntityLogicMethods<T, U, V>
        where W : class, IEntityInteractiveProperties<T, U, V>, IEntityInteractiveMethods<T, U, V, W>
    {
        ListEntityLogicProperties<S, R, T, U, V> Domains { get; set; }

        ListEntityInteractiveProperties<S, R, Q, T, U, V, W> Load(Q query, int maxdepth = 1, int top = 0);
        ListEntityInteractiveProperties<S, R, Q, T, U, V, W> Load(IEnumerable<W> list);
    }
}
