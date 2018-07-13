using library.Impl.Data.Sql;
using library.Interface.Data;
using library.Interface.Data.Sql;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace library.Impl.Data
{
    public class RepositoryBulk : BaseRepositoryBulk, IRepositoryBulk 
    {
        protected readonly ISqlCreator _creator;

        public RepositoryBulk(ISqlCreator creator)
            : base()
        {
            _creator = creator;
        }
        public RepositoryBulk(ConnectionStringSettings appconnectionstringsettings)
            : this(new SqlCreator(appconnectionstringsettings))
        {
        }
        public RepositoryBulk(string appconnectionstringname)
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
