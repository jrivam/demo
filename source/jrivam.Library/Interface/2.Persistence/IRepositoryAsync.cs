using jrivam.Library.Impl;
using jrivam.Library.Interface.Persistence.Sql;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Interface.Persistence
{
    public interface IRepositoryAsync : IRepository
    {
        Task<(Result result, IEnumerable<T> entities)> ExecuteQueryAsync<T>(string commandtext, CommandType commandtype = CommandType.Text, ISqlParameter[] parameters = null, int maxdepth = 1, IDbConnection connection = null, int commandtimeout = 30);

        Task<(Result result, int rows)> ExecuteNonQueryAsync(string commandtext, CommandType commandtype = CommandType.Text, ISqlParameter[] parameters = null, IDbConnection connection = null, IDbTransaction transaction = null, int commandtimeout = 30);

        Task<(Result result, T scalar)> ExecuteScalarAsync<T>(string commandtext, CommandType commandtype = CommandType.Text, ISqlParameter[] parameters = null, IDbConnection connection = null, IDbTransaction transaction = null, int commandtimeout = 30);
    }
}
