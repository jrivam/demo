using Library.Impl;
using Library.Impl.Domain;
using Library.Impl.Presentation;
using Library.Interface.Data.Query;
using Library.Interface.Data.Table;
using Library.Interface.Domain.Query;
using Library.Interface.Domain.Table;
using Library.Interface.Entities;
using Library.Interface.Presentation.Query;
using Library.Interface.Presentation.Table;
using System.Collections.Generic;

namespace Library.Interface.Presentation
{
    public interface IListPresentation<Q, R, S, T, U, V, W> : IList<W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : class, ITableModel<T, U, V, W>
        where S : IQueryData<T, U>
        where R : IQueryDomain<S, T, U, V>
        where Q : IQueryModel<R, S, T, U, V, W>
    {
        ListDomain<T, U, V> Domains { get; set; }

        (Result result, ListPresentation<Q, R, S, T, U, V, W> list) Refresh(int top = 0);
        (Result result, ListPresentation<Q, R, S, T, U, V, W> list) LoadQuery(Q query, int maxdepth = 1, int top = 0);

        ListPresentation<Q, R, S, T, U, V, W> Load(IEnumerable<W> list);
    }
}
