using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Interface.Persistence.Table
{
    public interface ITableDataMethodsAutoAsync<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        (Result result, U data) SelectQuery(int? commandtimeout = null, int maxdepth = 1, IDbConnection connection = null);

        Task<(Result result, U data)> SelectAsync(int? commandtimeout = null, IDbConnection connection = null);

        Task<(Result result, U data)> InsertAsync(int? commandtimeout = null, IDbConnection connection = null, IDbTransaction transaction = null);

        Task<(Result result, U data)> UpdateAsync(int? commandtimeout = null, IDbConnection connection = null, IDbTransaction transaction = null);

        Task<(Result result, U data)> DeleteAsync(int? commandtimeout = null, IDbConnection connection = null, IDbTransaction transaction = null);

        Task<(Result result, U data)> UpsertAsync(int? commandtimeout = null, IDbConnection connection = null, IDbTransaction transaction = null);
    }
}
