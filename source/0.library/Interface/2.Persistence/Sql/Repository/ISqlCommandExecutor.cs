using Library.Impl;
using Library.Impl.Persistence.Sql;
using Library.Interface.Entities;
using System.Collections.Generic;
using System.Data;

namespace Library.Interface.Persistence.Sql.Repository
{
    public interface ISqlCommandExecutor<T>
        where T : IEntity
    {
        (Result result, IEnumerable<T> entities) ExecuteQuery(SqlCommand sqlcommand, int maxdepth = 1, IList<T> entities = null);
        (Result result, IEnumerable<T> entities) ExecuteQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, int maxdepth = 1, IList<T> entities = null);
    }
}
