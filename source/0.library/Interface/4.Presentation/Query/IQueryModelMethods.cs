using Library.Impl;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;
using Library.Interface.Presentation.Table;
using System.Collections.Generic;

namespace Library.Interface.Presentation.Query
{
    public interface IQueryModelMethods<T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : class, ITableModel<T, U, V, W>
    {
        (Result result, W presentation) Retrieve(int maxdepth, W presentation = default(W));
        (Result result, IEnumerable<W> presentations) List(int maxdepth = 1, int top = 0, IListModel<T, U, V, W> presentations = null);
    }
}
