using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Interface.Persistence.Query
{
    public interface IQueryDataMethodsAsync<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        Task<(Result result, U data)> SelectSingleAsync(int maxdepth = 1, IDbConnection connection = null, int? commandtimeout = null);
        Task<(Result result, IEnumerable<U> datas)> SelectAsync(int maxdepth = 1, int top = 0, IDbConnection connection = null, int? commandtimeout = null);

        Task<(Result result, int rows)> UpdateAsync(IList<IColumnTable> columns, int maxdepth = 1, IDbConnection connection = null, IDbTransaction transaction = null, int? commandtimeout = null);

        Task<(Result result, int rows)> DeleteAsync(int maxdepth = 1, IDbConnection connection = null, IDbTransaction transaction = null, int? commandtimeout = null);
    }
}
