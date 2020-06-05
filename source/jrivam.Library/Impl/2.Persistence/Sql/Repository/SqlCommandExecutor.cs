using jrivam.Library.Impl.Persistence.Database;
using jrivam.Library.Interface.Entities.Reader;
using jrivam.Library.Interface.Persistence.Sql.Database;
using jrivam.Library.Interface.Persistence.Sql.Repository;
using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Impl.Persistence.Sql.Repository
{
    public class SqlCommandExecutor : DbCommandExecutor, ISqlCommandExecutor
    {
        protected readonly ISqlCreator _creator;

        public SqlCommandExecutor(ISqlCreator creator, IEntityReader reader)
            : base(reader)
        {
            _creator = creator;
        }

        public virtual (Result result, IEnumerable<T> entities) ExecuteQuery<T>(SqlCommand sqlcommand, int maxdepth = 1, ICollection<T> entities = null)
        {
            return ExecuteQuery(_creator.GetCommand(sqlcommand.Text, sqlcommand.Type, sqlcommand.Parameters), maxdepth, entities);
        }
        public virtual (Result result, IEnumerable<T> entities) ExecuteQuery<T>(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, int maxdepth = 1, ICollection<T> entities = null)
        {
            return ExecuteQuery(_creator.GetCommand(commandtext, commandtype, parameters), maxdepth, entities);
        }
    }
}
