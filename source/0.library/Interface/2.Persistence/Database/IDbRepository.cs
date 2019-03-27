using Library.Impl;
using Library.Interface.Entities;
using System.Collections.Generic;
using System.Data;

namespace Library.Interface.Persistence.Database
{
    public interface IDbRepository<T>
        where T : IEntity
    {
        (Result result, IEnumerable<T> entities) ExecuteQuery(IDbCommand command, string columnseparator, int maxdepth = 1, IList<T> entities = null);
    }
}
