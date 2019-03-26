using Library.Impl.Data.Database;
using Library.Interface.Data.Sql;
using Library.Interface.Entities;
using Library.Interface.Entities.Reader;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Library.Impl.Data.Sql
{
    public class SqlRepository<T> : DbRepository<T>, ISqlRepository<T> 
        where T : IEntity
    {
        protected readonly ISqlCreator _creator;

        public SqlRepository(ISqlCreator creator, IReaderEntity<T> reader)
            : base(reader)
        {
            _creator = creator;
        }

        public SqlRepository(IReaderEntity<T> reader, ConnectionStringSettings connectionstringsettings)
            : this(new SqlCreator(connectionstringsettings), reader)
        {
        }

        public SqlRepository(IReaderEntity<T> reader, string appconnectionstringname)
            : this(reader, ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appconnectionstringname]])
        {
        }

        public virtual (Result result, IEnumerable<T> entities) ExecuteQuery(string columnseparator, string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, int maxdepth = 1, IList<T> entities = null)
        {
            return ExecuteQuery(_creator.GetCommand(commandtext, commandtype, parameters), columnseparator, maxdepth, entities);
        }
    }
}
