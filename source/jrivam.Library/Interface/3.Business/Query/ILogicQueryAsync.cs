using jrivam.Library.Impl;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Interface.Business.Query
{
    public interface ILogicQueryAsync<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        Task<(Result result, V domain)> RetrieveAsync(IQueryDomain<T, U, V> query, int maxdepth = 1, IDbConnection connection = null, int? commandtimeout = null);

        Task<(Result result, IEnumerable<V> domains)> ListAsync(IQueryDomain<T, U, V> query, int maxdepth = 1, int top = 0, IDbConnection connection = null, int? commandtimeout = null);
    }
}
