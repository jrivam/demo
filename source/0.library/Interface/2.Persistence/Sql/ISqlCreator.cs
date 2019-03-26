using Library.Impl.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;

namespace Library.Interface.Data.Sql
{
    public interface ISqlCreator
    {
        IDbDataParameter GetParameter(string name, Type type, object value, ParameterDirection direction = ParameterDirection.Input);
        IDbCommand GetCommand(string commandtext = "", CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null);
    }
}
