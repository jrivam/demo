using jrivam.Library.Impl;
using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Interface.Persistence.Database
{
    public interface IDbCommandExecutor
    {
        (Result result, IEnumerable<T> entities) ExecuteQuery<T>(IDbCommand command, int maxdepth = 1, ICollection<T> entities = null);
    }
}
