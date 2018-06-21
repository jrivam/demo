using library.Impl;
using library.Impl.Data.Sql;
using System.Collections.Generic;
using System.Data;

namespace library.Interface.Data
{
    public interface IRepositoryBase
    {
        (Result result, int rows) ExecuteNonQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null);
        (Result result, int rows) ExecuteNonQuery(IDbCommand command);

        (Result result, object scalar) ExecuteScalar(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null);
        (Result result, object scalar) ExecuteScalar(IDbCommand command);
    }
}
