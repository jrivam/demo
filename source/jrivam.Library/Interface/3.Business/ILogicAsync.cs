using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Interface.Business
{
    public interface ILogicAsync<T, U> : ILogic<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        Task<(Result result, IEnumerable<U> datas)> ListAsync(IQueryData<T, U> query, int maxdepth = 1, int top = 0, IDbConnection connection = null, int? commandtimeout = null);

        Task<(Result result, U data)> LoadQueryAsync(U data, int maxdepth = 1, IDbConnection connection = null, int? commandtimeout = null);

        Task<(Result result, U data)> LoadAsync(U data, IDbConnection connection = null, int? commandtimeout = null);

        Task<(Result result, U data)> SaveAsync(U data, IDbConnection connection = null, IDbTransaction transaction = null, int? commandtimeout = null);

        Task<(Result result, U data)> EraseAsync(U data, IDbConnection connection = null, IDbTransaction transaction = null, int? commandtimeout = null);
    }
}
