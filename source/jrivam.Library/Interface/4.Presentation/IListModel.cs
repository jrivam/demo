using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation.Table;
using System.Collections.Generic;

namespace jrivam.Library.Interface.Presentation
{
    public interface IListModel<T, U, V, W> : IList<W>
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
