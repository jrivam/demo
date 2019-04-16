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
    public class SqlRepository<T> : DbRepository<T>, ISqlRepository<T> 
        where T : IEntity
    {
        protected readonly ISqlCreator _creator;

        public SqlRepository(ISqlCreator creator, IReader<T> reader)
            : base(reader)
        {
            _creator = creator;
        }

        public SqlRepository(IReader<T> reader, ConnectionStringSettings connectionstringsettings)
            : this(new SqlCreator(connectionstringsettings), reader)
        {
        }

        public SqlRepository(IReader<T> reader, string appconnectionstringname)
            : this(reader, ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appconnectionstringname]])
        {
        }

        public virtual (Result result, IEnumerable<T> entities) ExecuteQuery(string columnseparator, string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, int maxdepth = 1, IList<T> entities = null)
        {
            return ExecuteQuery(_creator.GetCommand(commandtext, commandtype, parameters), columnseparator, maxdepth, entities);
        }
    }
}
