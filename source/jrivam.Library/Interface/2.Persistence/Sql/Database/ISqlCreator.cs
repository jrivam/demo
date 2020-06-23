using jrivam.Library.Impl.Persistence.Sql;
using System;
using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Interface.Persistence.Sql.Database
{
    public interface ISqlCreator
    {
        IDbConnection GetConnection(string providername, string connectionstring);
            
        IDbDataParameter GetParameter(string providername, string name, Type type, object value, ParameterDirection direction = ParameterDirection.Input);

        IDbCommand GetCommand(string providername, string commandtext = "", CommandType commandtype = CommandType.Text);
        IDbCommand GetCommand(string providername, string commandtext = "", CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null);
        IDbCommand GetCommand(string providername, string commandtext = "", CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, string connectionstring = "");
    }
}
