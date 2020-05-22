using Library.Interface.Entities;
using Library.Interface.Persistence.Table;

namespace Library.Interface.Persistence
{
    public interface IListDataEdit<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        void ItemEdit(U olddata, U newdata);
        bool ItemAdd(U data);
        bool ItemRemove(U data);
    }
}
