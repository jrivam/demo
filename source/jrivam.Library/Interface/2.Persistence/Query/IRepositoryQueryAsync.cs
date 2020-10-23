using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Sql;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Interface.Persistence.Query
{
    public interface IRepositoryQueryAsync<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        Task<(Result result, U data)> SelectSingleAsync(IQueryData<T, U> query, int maxdepth = 1, IDbConnection connection = null, int commandtimeout = 30);
        Task<(Result result, IEnumerable<U> datas)> SelectAsync(IQueryData<T, U> query, int maxdepth = 1, int top = 0, IDbConnection connection = null, int commandtimeout = 30);
        Task<(Result result, IEnumerable<U> datas)> SelectAsync(string commandtext, CommandType commandtype = CommandType.Text, IList<ISqlParameter> parameters = null, int maxdepth = 1, IDbConnection connection = null, int commandtimeout = 30);

        Task<(Result result, int rows)> UpdateAsync(IQueryData<T, U> query, IList<IColumnTable> tablecolumns, int maxdepth = 1, IDbConnection connection = null, IDbTransaction transaction = null, int commandtimeout = 30);

        Task<(Result result, int rows)> DeleteAsync(IQueryData<T, U> query, int maxdepth = 1, IDbConnection connection = null, IDbTransaction transaction = null, int commandtimeout = 30);
    }
}
