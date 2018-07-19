using library.Impl;
using library.Impl.Data.Sql;
using System.Collections.Generic;
using System.Data;

namespace library.Interface.Entities.Repository
{
    public interface IRepository<T>
        where T : IEntity
    {
        (Result result, IEnumerable<T> entities) ExecuteQuery(string columnseparator, string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, int maxdepth = 1, IList<T> entities = null);
    }
}
