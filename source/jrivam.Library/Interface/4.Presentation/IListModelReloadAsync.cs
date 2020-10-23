using jrivam.Library.Impl;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation.Table;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Interface.Presentation
{
    public interface IListModelReloadAsync<T, U, V, W> : IListModelReload<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        Task<(Result result, IListModel<T, U, V, W> models)> RefreshAsync(int top = 0, IDbConnection connection = null, int? commandtimeout = null);
    }
}
