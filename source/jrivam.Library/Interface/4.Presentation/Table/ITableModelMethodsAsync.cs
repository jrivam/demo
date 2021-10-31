using jrivam.Library.Impl;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Interface.Presentation.Table
{
    public interface ITableModelMethodsAsync<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        Task<(Result result, W model)> LoadAsync(IDbConnection connection = null, int? commandtimeout = null);

        Task<(Result result, W model)> LoadQueryAsync(int maxdepth = 1, IDbConnection connection = null, int? commandtimeout = null);

        Task<(Result result, W model)> SaveAsync(IDbConnection connection = null, IDbTransaction transaction = null, int? commandtimeout = null);

        Task<(Result result, W model)> EraseAsync(IDbConnection connection = null, IDbTransaction transaction = null, int? commandtimeout = null);
    }
}
