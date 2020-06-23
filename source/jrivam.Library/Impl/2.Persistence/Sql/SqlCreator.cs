using jrivam.Library.Interface.Persistence.Database;
using jrivam.Library.Interface.Persistence.Sql.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace jrivam.Library.Impl.Persistence.Sql
{
    public class SqlCreator : ISqlCreator
    {
        protected readonly IDbObjectCreator _dbcreator;

        public SqlCreator(IDbObjectCreator objectcreator)
        {
            _dbcreator = objectcreator;
        }

        public virtual IDbConnection GetConnection(string providername, string connectionstring)
        {
            return _dbcreator?.GetConnection(providername, connectionstring);
        }

        public virtual IDbDataParameter GetParameter(string providername, 
            string name, Type type, object value, ParameterDirection direction = ParameterDirection.Input)
        {
            var parameter = _dbcreator?.GetParameter(providername);

            parameter.ParameterName = name;
            parameter.DbType = Persistence.Helper.TypeToDbType[type];
            parameter.Value = value ?? DBNull.Value;
            parameter.Direction = direction;

            return parameter;
        }

        public virtual IDbCommand GetCommand(string providername,
            string commandtext = "", CommandType commandtype = CommandType.Text)
        {
            var command = _dbcreator?.GetCommand(providername);

            command.CommandText = commandtext;
            command.CommandType = commandtype;

            return command;
        }
        public virtual IDbCommand GetCommand(string providername, 
            string commandtext = "", CommandType commandtype = CommandType.Text, 
            IList<SqlParameter> parameters = null)
        {
            var command = GetCommand(providername, commandtext, commandtype);

            parameters?.ToList()?.ForEach(p =>
            {
                command.Parameters.Add(GetParameter(providername, p.Name, p.Type, p.Value, p.Direction));
            });

            return command;
        }
        public virtual IDbCommand GetCommand(string providername, 
            string commandtext = "", CommandType commandtype = CommandType.Text, 
            IList<SqlParameter> parameters = null,
            string connectionstring = "")
        {            
            var command = GetCommand(providername, commandtext, commandtype, parameters);

            command.Connection = GetConnection(providername, connectionstring);

            return command;
        }
    }
}
