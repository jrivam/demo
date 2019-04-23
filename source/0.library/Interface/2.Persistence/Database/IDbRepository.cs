using Library.Impl;
using System.Collections.Generic;
using System.Data;

namespace Library.Interface.Persistence.Database
{
    public interface IDbRepository<T>
    {
        (Result result, IEnumerable<T> entities) ExecuteQuery(IDbCommand command, int maxdepth = 1, IList<T> entities = null);
    }
}
