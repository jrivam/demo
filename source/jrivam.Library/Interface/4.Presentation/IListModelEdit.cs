using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation.Table;
using System.Windows.Input;

namespace jrivam.Library.Interface.Presentation
{
    public interface IListModelEdit<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        ICommand AddCommand { get; }

        void ItemEdit(W oldmodel, W newmodel);
        bool ItemAdd(W model);
        bool ItemRemove(W model);
    }
}
