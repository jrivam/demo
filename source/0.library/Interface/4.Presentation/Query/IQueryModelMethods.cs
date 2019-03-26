using Library.Impl;
using Library.Interface.Data.Table;
using Library.Interface.Domain.Table;
using Library.Interface.Entities;
using Library.Interface.Presentation.Table;
using System.Collections.Generic;

namespace Library.Interface.Presentation.Query
{
    public interface IQueryModelMethods<T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        (Result result, W presentation) Retrieve(int maxdepth, W presentation = default(W));
        (Result result, IEnumerable<W> presentations) List(int maxdepth = 1, int top = 0, IList<W> presentations = null);
    }
}
