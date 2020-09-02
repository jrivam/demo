using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Data;

namespace jrivam.Library.Interface.Persistence
{
    public interface IListDataReload<T, U> : IListDataEdit<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        (Result result, IListData<T, U> datas) Refresh(int? commandtimeout = null, int top = 0, IDbConnection connection = null);
    }
}
