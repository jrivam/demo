using library.Impl;
using System.Collections.Generic;
using System.Data;

namespace library.Interface.Entities.Repository
{
    public interface IDbRepository<T>
        where T : IEntity
    {
        (Result result, IEnumerable<T> entities) ExecuteQuery(IDbCommand command, string columnseparator, int maxdepth = 1, IList<T> entities = null);
    }
}
