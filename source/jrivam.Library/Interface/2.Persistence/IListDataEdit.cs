using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Interface.Persistence
{
    public interface IListDataEdit<T, U> : IListData<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        void ItemEdit(U olddata, U newdata);
        bool ItemAdd(U data);
        bool ItemRemove(U data);
    }
}
