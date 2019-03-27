using Library.Impl;
using Library.Interface.Data.Table;
using Library.Interface.Domain.Table;
using Library.Interface.Entities;
using Library.Interface.Presentation.Table;
using System.Windows.Input;

namespace Library.Interface.Presentation
{
    public interface IListPresentationRefresh<T, U, V, W> : IListPresentation<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : class, ITableModel<T, U, V, W>
    {
        ICommand RefreshCommand { get; }

        (Result result, IListPresentation<T, U, V, W> list) Refresh(int top = 0);
    }
}
