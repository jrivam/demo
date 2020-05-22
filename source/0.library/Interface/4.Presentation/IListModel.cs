using Library.Interface.Business;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;
using Library.Interface.Presentation.Table;
using System.Collections.Generic;

namespace Library.Interface.Presentation
{
    public interface IListModel<T, U, V, W> : IList<W>, IListModelEdit<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        IListDomain<T, U, V> Domains { get; }

        string Name { get; }

        IListModel<T, U, V, W> Load(IEnumerable<W> list);
    }
}
