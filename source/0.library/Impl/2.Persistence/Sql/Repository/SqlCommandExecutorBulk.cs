using Library.Impl.Persistence.Database;
using Library.Interface.Persistence.Sql.Database;
using Library.Interface.Persistence.Sql.Repository;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Library.Impl.Persistence.Sql.Repository
{
    public class SqlCommandExecutorBulk : DbCommandExecutorBulk, ISqlCommandExecutorBulk 
    {
        protected readonly ISqlCreator _creator;

        public SqlCommandExecutorBulk(ISqlCreator creator)
            : base()
        {
            _creator = creator;
        }

        public SqlCommandExecutorBulk(ConnectionStringSettings appconnectionstringsettings)
            : this(new SqlCreator(appconnectionstringsettings))
        {
        }

        public SqlCommandExecutorBulk(string appconnectionstringname)
            : this(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appconnectionstringname]])
        {
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
