using Library.Impl.Data.Database;
using Library.Interface.Data.Sql;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Library.Impl.Data.Sql
{
    public class SqlRepositoryBulk : DbRepositoryBulk, ISqlRepositoryBulk 
    {
        protected readonly ISqlCreator _creator;

        public SqlRepositoryBulk(ISqlCreator creator)
            : base()
        {
            _creator = creator;
        }

        public SqlRepositoryBulk(ConnectionStringSettings appconnectionstringsettings)
            : this(new SqlCreator(appconnectionstringsettings))
        {
        }

        public SqlRepositoryBulk(string appconnectionstringname)
            : this(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appconnectionstringname]])
        {
        }

        public virtual (Result result, int rows) ExecuteNonQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null)
        {
            return ExecuteNonQuery(_creator.GetCommand(commandtext, commandtype, parameters));
        }
        public virtual (Result result, object scalar) ExecuteScalar(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null)
        {
            return ExecuteScalar(_creator.GetCommand(commandtext, commandtype, parameters));
        }
    }
}
