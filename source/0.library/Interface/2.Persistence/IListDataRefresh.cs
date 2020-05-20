using Library.Impl;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;

namespace Library.Interface.Persistence
{
    public interface IListDataRefresh<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        (Result result, IListData<T, U> datas) Refresh(int top = 0);
    }
}
