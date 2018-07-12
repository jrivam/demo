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
    public interface IListTableInteractiveProperties<S, R, Q, T, U, V, W>
        where S : IQueryRepositoryMethods<T, U>
        where R : IQueryLogicMethods<T, U, V>
        where Q : IQueryInteractiveMethods<T, U, V, W>
        where T : IEntity
        where U : ITableRepositoryProperties<T>
        where V : ITableLogicProperties<T, U>, ITableLogicMethods<T, U, V>
        where W : class, ITableInteractiveProperties<T, U, V>, ITableInteractiveMethods<T, U, V, W>
    {
        ListTableLogicProperties<S, R, T, U, V> Domains { get; set; }

        ListTableInteractiveProperties<S, R, Q, T, U, V, W> Load(Q query, int maxdepth = 1, int top = 0);
        ListTableInteractiveProperties<S, R, Q, T, U, V, W> Load(IEnumerable<W> list);
    }
}
