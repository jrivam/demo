using Library.Impl;
using Library.Interface.Data.Query;
using Library.Interface.Data.Table;
using Library.Interface.Domain.Query;
using Library.Interface.Domain.Table;
using Library.Interface.Entities;
using Library.Interface.Presentation.Table;
using System.Collections.Generic;

namespace Library.Interface.Presentation.Query
{
    public interface IInteractiveQuery<R, S, T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
        where S : IQueryData<T, U>
        where R : IQueryDomain<S, T, U, V>
    {
        (Result result, W presentation) Retrieve(IQueryModel<R, S, T, U, V, W> query, int maxdepth = 1, W presentation = default(W));
        (Result result, IEnumerable<W> presentations) List(IQueryModel<R, S, T, U, V, W> query, int maxdepth = 1, int top = 0, IList<W> presentations = null);
    }
}
