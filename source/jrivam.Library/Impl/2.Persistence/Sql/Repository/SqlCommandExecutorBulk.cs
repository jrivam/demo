﻿using jrivam.Library.Impl.Persistence.Database;
using jrivam.Library.Interface.Persistence.Sql.Database;
using jrivam.Library.Interface.Persistence.Sql.Repository;
using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Impl.Persistence.Sql.Repository
{
    public class SqlCommandExecutorBulk : DbCommandExecutorBulk, ISqlCommandExecutorBulk 
    {
        protected readonly ISqlCreator _creator;

        public SqlCommandExecutorBulk(ISqlCreator creator)
            : base()
        {
            _creator = creator;
        }

        public virtual (Result result, int rows) ExecuteNonQuery(SqlCommand sqlcommand)
        {
            return ExecuteNonQuery(sqlcommand.Text, sqlcommand.Type, sqlcommand.Parameters);
        }
        public virtual (Result result, int rows) ExecuteNonQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null)
        {
            return ExecuteNonQuery(_creator.GetCommand(commandtext, commandtype, parameters));
        }

        public virtual (Result result, object scalar) ExecuteScalar(SqlCommand sqlcommand)
        {
            return ExecuteScalar(sqlcommand.Text, sqlcommand.Type, sqlcommand.Parameters);
        }
        public virtual (Result result, object scalar) ExecuteScalar(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null)
        {
            return ExecuteScalar(_creator.GetCommand(commandtext, commandtype, parameters));
        }
    }
}
