using jrivam.Library.Impl;
using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Interface.Entities;
using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Interface.Persistence.Sql.Repository
{
    public interface ISqlCommandExecutor<T>
        where T : IEntity
    {
        (Result result, IEnumerable<T> entities) ExecuteQuery(SqlCommand sqlcommand, int maxdepth = 1, ICollection<T> entities = null);
        (Result result, IEnumerable<T> entities) ExecuteQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, int maxdepth = 1, ICollection<T> entities = null);
    }
}
