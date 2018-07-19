using library.Impl.Data.Sql;
using library.Interface.Data.Sql;
using library.Interface.Entities;
using library.Interface.Entities.Reader;
using library.Interface.Entities.Repository;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace library.Impl.Entities.Repository
{
    public class Repository<T> : DbRepository<T>, IRepository<T> 
        where T : IEntity
    {

        public Repository(ISqlCreator creator, IReaderEntity<T> reader)
            : base(creator, reader)
        {
        }

        public Repository(IReaderEntity<T> reader, ConnectionStringSettings connectionstringsettings)
            : this(new SqlCreator(connectionstringsettings), reader)
        {
        }
        public Repository(IReaderEntity<T> reader, string appconnectionstringname)
            : this(reader, ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appconnectionstringname]])
        {
        }

        public virtual (Result result, IEnumerable<T> entities) ExecuteQuery(string columnseparator, string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, int maxdepth = 1, IList<T> entities = null)
        {
            return ExecuteQuery(_creator.GetCommand(commandtext, commandtype, parameters), columnseparator, maxdepth, entities);
        }
    }
}
