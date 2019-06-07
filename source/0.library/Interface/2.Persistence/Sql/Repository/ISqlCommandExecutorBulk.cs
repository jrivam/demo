using Library.Impl;
using Library.Impl.Persistence.Sql;
using System.Collections.Generic;
using System.Data;

namespace Library.Interface.Persistence.Sql.Repository
{
    public interface ISqlCommandExecutorBulk
    {
        (Result result, int rows) ExecuteNonQuery(SqlCommand sqlcommand);
        (Result result, int rows) ExecuteNonQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null);

        (Result result, object scalar) ExecuteScalar(SqlCommand sqlcommand);
        (Result result, object scalar) ExecuteScalar(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null);
    }
}
