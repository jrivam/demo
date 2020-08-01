using System;
using System.Data;

namespace jrivam.Library.Interface.Persistence.Sql.Database
{
    public interface ISqlCreator
    {
        IDbConnection GetConnection(string providername, string connectionstring);

        IDbDataParameter GetParameter(string providername, string name, Type type, object value, ParameterDirection direction = ParameterDirection.Input);

        IDbCommand GetCommand(string providername, string commandtext = "", CommandType commandtype = CommandType.Text, params ISqlParameter[] parameters);
    }
}
