using Library.Impl.Persistence.Database;
using Library.Interface.Entities;
using Library.Interface.Entities.Reader;
using Library.Interface.Persistence.Sql.Database;
using Library.Interface.Persistence.Sql.Repository;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Library.Impl.Persistence.Sql.Repository
{
    public class SqlCommandExecutor<T> : DbCommandExecutor<T>, ISqlCommandExecutor<T> 
        where T : IEntity
    {
        protected readonly ISqlCreator _creator;

        public SqlCommandExecutor(ISqlCreator creator, IReader<T> reader)
            : base(reader)
        {
            _creator = creator;
        }

        public SqlCommandExecutor(IReader<T> reader, ConnectionStringSettings connectionstringsettings)
            : this(new SqlCreator(connectionstringsettings), reader)
        {
        }

        public SqlCommandExecutor(IReader<T> reader, string appconnectionstringname)
            : this(reader, ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appconnectionstringname]])
        {
        }

        public virtual (Result result, IEnumerable<T> entities) ExecuteQuery(SqlCommand sqlcommand, int maxdepth = 1, ICollection<T> entities = null)
        {
            return ExecuteQuery(_creator.GetCommand(sqlcommand.Text, sqlcommand.Type, sqlcommand.Parameters), maxdepth, entities);
        }
        public virtual (Result result, IEnumerable<T> entities) ExecuteQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, int maxdepth = 1, ICollection<T> entities = null)
        {
            return ExecuteQuery(_creator.GetCommand(commandtext, commandtype, parameters), maxdepth, entities);
        }
    }
}
