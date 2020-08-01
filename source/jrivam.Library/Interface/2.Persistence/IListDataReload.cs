using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Interface.Persistence
{
    public interface IListDataReload<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        (Result result, IListData<T, U> datas) Refresh(int top = 0);
    }
}
