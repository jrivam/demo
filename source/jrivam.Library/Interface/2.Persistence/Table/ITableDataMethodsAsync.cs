using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Interface.Persistence.Table
{
    public interface ITableDataMethodsAsync<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        Task<(Result result, U data)> SelectQueryAsync(int maxdepth = 1, IDbConnection connection = null, int? commandtimeout = null);        

        Task<(Result result, U data)> SelectAsync(IDbConnection connection = null, int? commandtimeout = null);

        Task<(Result result, U data)> InsertAsync(IDbConnection connection = null, IDbTransaction transaction = null, int? commandtimeout = null);

        Task<(Result result, U data)> UpdateAsync(IDbConnection connection = null, IDbTransaction transaction = null, int? commandtimeout = null);

        Task<(Result result, U data)> DeleteAsync(IDbConnection connection = null, IDbTransaction transaction = null, int? commandtimeout = null);

        Task<(Result result, U data)> UpsertAsync(IDbConnection connection = null, IDbTransaction transaction = null, int? commandtimeout = null);
    }
}
