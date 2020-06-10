using jrivam.Library.Impl;
using jrivam.Library.Impl.Persistence.Sql;
using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Interface.Persistence
{
    public interface IRepository
    {
        (Result result, IEnumerable<T> entities) ExecuteQuery<T>(SqlCommand sqlcommand, int maxdepth = 1, ICollection<T> entities = null);
        (Result result, IEnumerable<T> entities) ExecuteQuery<T>(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, int maxdepth = 1, ICollection<T> entities = null);

        (Result result, int rows) ExecuteNonQuery(SqlCommand sqlcommand);
        (Result result, int rows) ExecuteNonQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null);

        (Result result, object scalar) ExecuteScalar(SqlCommand sqlcommand);
        (Result result, object scalar) ExecuteScalar(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null);
    }
}
