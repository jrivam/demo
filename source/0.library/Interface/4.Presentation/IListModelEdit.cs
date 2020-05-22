using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;
using Library.Interface.Presentation.Table;
using System.Windows.Input;

namespace Library.Interface.Presentation
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
