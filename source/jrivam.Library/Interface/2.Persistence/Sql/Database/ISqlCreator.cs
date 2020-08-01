using System;
using System.Data;

namespace jrivam.Library.Interface.Persistence.Sql.Database
{
    public interface ISqlCreator
    {
        IDbConnection GetConnection(string providername, string connectionstring);
            
        IDbDataParameter GetParameter(string providername, string name, Type type, object value, ParameterDirection direction = ParameterDirection.Input);

        IDbCommand GetCommand(string providername, string commandtext = "", CommandType commandtype = CommandType.Text);
        IDbCommand GetCommand(string providername, string commandtext = "", CommandType commandtype = CommandType.Text, params ISqlParameter[] parameters);
        IDbCommand GetCommand(string providername, string connectionstring, string commandtext = "", CommandType commandtype = CommandType.Text, ISqlParameter[] parameters = null);
    }
}
