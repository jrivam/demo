using jrivam.Library.Impl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Interface.Persistence.Database
{
    public interface IDbCommandExecutorAsync : IDbCommandExecutor
    {
        Task<(Result result, IEnumerable<T> entities)> ExecuteQueryAsync<T>(IDbCommand command, Func<T, IDataReader, T> reader, CommandBehavior commandbehavior = CommandBehavior.Default);

        Task<(Result result, int rows)> ExecuteNonQueryAsync(IDbCommand command);
        
        Task<(Result result, T scalar)> ExecuteScalarAsync<T>(IDbCommand command);
    }
}
