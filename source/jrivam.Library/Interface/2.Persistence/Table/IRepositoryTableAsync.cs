using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Sql;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Interface.Persistence.Table
{
    public interface IRepositoryTableAsync<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        Task<(Result result, U data)> SelectAsync(U data, IDbConnection connection = null, int commandtimeout = 30);
        Task<(Result result, U data)> SelectAsync(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<ISqlParameter> parameters = null, IDbConnection connection = null, int commandtimeout = 30);

        Task<(Result result, U data)> InsertAsync(U data, IDbConnection connection = null, IDbTransaction transaction = null, int commandtimeout = 30);
        Task<(Result result, U data)> InsertAsync(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<ISqlParameter> parameters = null, IDbConnection connection = null, IDbTransaction transaction = null, int commandtimeout = 30);

        Task<(Result result, U data)> UpdateAsync(U data, IDbConnection connection = null, IDbTransaction transaction = null, int commandtimeout = 30);
        Task<(Result result, U data)> UpdateAsync(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<ISqlParameter> parameters = null, IDbConnection connection = null, IDbTransaction transaction = null, int commandtimeout = 30);

        Task<(Result result, U data)> DeleteAsync(U data, IDbConnection connection = null, IDbTransaction transaction = null, int commandtimeout = 30);
        Task<(Result result, U data)> DeleteAsync(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<ISqlParameter> parameters = null, IDbConnection connection = null, IDbTransaction transaction = null, int commandtimeout = 30);
    }
}
