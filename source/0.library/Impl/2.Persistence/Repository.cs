using Library.Impl.Persistence.Sql;
using Library.Interface.Entities;
using Library.Interface.Persistence;
using Library.Interface.Persistence.Sql.Repository;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Library.Impl.Persistence
{
    public class Repository<T> : IRepository<T>
        where T : IEntity
    {
        protected readonly ISqlCommandExecutor<T> _sqlcommandexecutor;
        protected readonly ISqlCommandExecutorBulk _sqlcommandexecutorbulk;

        public Repository(ISqlCommandExecutor<T> sqlcommandexecutor, ISqlCommandExecutorBulk sqlcommandexecutorbulk)
        {
            _sqlcommandexecutor = sqlcommandexecutor;
            _sqlcommandexecutorbulk = sqlcommandexecutorbulk;
        }

        public virtual (Result result, IEnumerable<T> entities) Select(string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null, int maxdepth = 1, IList<T> entities = null)
        {
            var executequery = _sqlcommandexecutor.ExecuteQuery(commandtext, commandtype, parameters, maxdepth, entities);
            if (executequery.entities?.Count() == 0)
            {
                executequery.result.Messages.Add((ResultCategory.Information, "Select", "No rows found"));
            }

            return executequery;
        }
        public virtual (Result result, object scalar) Insert(string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var executescalar = _sqlcommandexecutorbulk.ExecuteScalar(commandtext, commandtype, parameters);
            if (executescalar.scalar == null)
            {
                executescalar.result.Messages.Add((ResultCategory.Information, "Insert", "No rows affected"));
            }

            return executescalar;
        }
        public virtual (Result result, int rows) Update(string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var executenonquery = _sqlcommandexecutorbulk.ExecuteNonQuery(commandtext, commandtype, parameters);
            if (executenonquery.rows == 0)
            {
                executenonquery.result.Messages.Add((ResultCategory.Information, "Update", "No rows affected"));
            }

            return executenonquery;
        }
        public virtual (Result result, int rows) Delete(string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var executenonquery = _sqlcommandexecutorbulk.ExecuteNonQuery(commandtext, commandtype, parameters);
            if (executenonquery.rows == 0)
            {
                executenonquery.result.Messages.Add((ResultCategory.Information, "Delete", "No rows affected"));
            }

            return executenonquery;
        }
    }
}
