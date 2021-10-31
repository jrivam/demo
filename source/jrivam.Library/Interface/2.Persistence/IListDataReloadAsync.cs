using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Interface.Persistence
{
    public interface IListDataReloadAsync<T, U> : IListDataEdit<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        Task<(Result result, IListData<T, U> datas)> RefreshAsync(int top = 0, IDbConnection connection = null, int? commandtimeout = null);
    }
}
