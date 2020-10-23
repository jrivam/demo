using jrivam.Library.Impl;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Interface.Presentation.Table
{
    public interface IInteractiveTableAsync<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        Task<(Result result, W model)> LoadAsync(W model, IDbConnection connection = null, int? commandtimeout = null);

        Task<(Result result, W model)> LoadQueryAsync(W model, int maxdepth = 1, IDbConnection connection = null, int? commandtimeout = null);

        Task<(Result result, W model)> SaveAsync(W model, IDbConnection connection = null, IDbTransaction transaction = null, int? commandtimeout = null);

        Task<(Result result, W model)> EraseAsync(W model, IDbConnection connection = null, IDbTransaction transaction = null, int? commandtimeout = null);
    }
}
