using jrivam.Library.Impl;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation.Table;
using System.Windows.Input;

namespace jrivam.Library.Interface.Presentation
{
    public interface IListModelRefresh<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        ICommand RefreshCommand { get; }

        (Result result, IListModel<T, U, V, W> models) Refresh(int top = 0);
    }
}
