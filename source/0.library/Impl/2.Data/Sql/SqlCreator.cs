using library.Interface.Data;
using library.Interface.Data.Sql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace library.Impl.Data.Sql
{
    public class SqlCreator : ISqlCreator
    {
        protected readonly IDbObjectCreator _dbcreator;

        public SqlCreator(IDbObjectCreator objectcreator)
        {
            _dbcreator = objectcreator;
        }
        public SqlCreator(ConnectionStringSettings connectionstringsettings)
            : this(new DbObjectCreator(connectionstringsettings))
        {
        }

        public virtual IDbDataParameter GetParameter(string name, Type type, object value, ParameterDirection direction = ParameterDirection.Input)
        {
            var parameter = _dbcreator?.Parameter;

            parameter.ParameterName = name;
            parameter.DbType = Data.Helper.TypeToDbType[type];
            parameter.Value = value ?? DBNull.Value;
            parameter.Direction = direction;

            return parameter;
        }
        public virtual IDbCommand GetCommand(string commandtext = "", CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null)
        {
            var command = _dbcreator?.Command;

            command.CommandText = commandtext;
            command.CommandType = commandtype;

            parameters?.ToList()?.ForEach(p => 
            {
                command.Parameters.Add(GetParameter(p.Name, p.Type, p.Value, p.Direction));
            });

            command.Connection = _dbcreator?.Connection;

            return command;
        }
    }
}
