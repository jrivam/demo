using Library.Impl;
using Library.Impl.Data.Sql;
using Library.Interface.Entities;
using System.Collections.Generic;
using System.Data;

namespace Library.Interface.Data.Sql.Repository
{
    public interface ISqlRepository<T>
        where T : IEntity
    {
        (Result result, IEnumerable<T> entities) ExecuteQuery(string columnseparator, string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, int maxdepth = 1, IList<T> entities = null);
    }
}
