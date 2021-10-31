using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Interface.Persistence.Table
{
    public interface ITableDataMethodsCommandAsync<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        Task<(Result result, U data)> SelectCommandAsync(IDbConnection connection = null);

        Task<(Result result, U data)> InsertCommandAsync(IDbConnection connection = null, IDbTransaction transaction = null);

        Task<(Result result, U data)> UpdateCommandAsync(IDbConnection connection = null, IDbTransaction transaction = null);

        Task<(Result result, U data)> DeleteCommandAsync(IDbConnection connection = null, IDbTransaction transaction = null);

        Task<(Result result, U data)> UpsertCommandAsync(IDbConnection connection = null, IDbTransaction transaction = null);
    }
}
