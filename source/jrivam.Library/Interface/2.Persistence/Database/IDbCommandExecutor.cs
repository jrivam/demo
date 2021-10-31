using jrivam.Library.Impl;
using System;
using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Interface.Persistence.Database
{
    public interface IDbCommandExecutor
    {
        (Result result, IEnumerable<T> entities) ExecuteQuery<T>(IDbCommand command, Func<T, IDataReader, T> reader);

        (Result result, int rows) ExecuteNonQuery(IDbCommand command);
        (Result result, T scalar) ExecuteScalar<T>(IDbCommand command);
    }
}
