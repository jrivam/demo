using Library.Impl;
using Library.Impl.Data.Sql;
using System.Collections.Generic;
using System.Data;

namespace Library.Interface.Data.Sql
{
    public interface ISqlRepositoryBulk
    {
        (Result result, int rows) ExecuteNonQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null);
        (Result result, object scalar) ExecuteScalar(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null);
    }
}
