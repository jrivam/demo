using library.Interface.Persistence;
using Library.Impl;
using Library.Impl.Persistence.Sql;
using Library.Interface.Entities;
using Library.Interface.Persistence.Sql.Repository;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace library.Impl.Persistence
{
    public class Repository<T> : IRepository<T>
        where T : IEntity
    {
        protected readonly ISqlRepository<T> _sqlrepository;
        protected readonly ISqlRepositoryBulk _sqlrepositorybulk;

        public Repository(ISqlRepository<T> sqlrepository, ISqlRepositoryBulk sqlrepositorybulk)
        {
            _sqlrepository = sqlrepository;
            _sqlrepositorybulk = sqlrepositorybulk;
        }

        public virtual (Result result, IEnumerable<T> entities) Select(string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null, int maxdepth = 1, IList<T> entities = null)
        {
            var executequery = _sqlrepository.ExecuteQuery(commandtext, commandtype, parameters, maxdepth, entities);

            if (executequery.entities?.Count() == 0)
            {
                executequery.result.Messages.Add((ResultCategory.Information, "Select", "No rows found"));
            }

            return executequery;
        }
        public virtual (Result result, object scalar) Insert(string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var executescalar = _sqlrepositorybulk.ExecuteScalar(commandtext, commandtype, parameters);

            if (executescalar.scalar == null)
            {
                executescalar.result.Messages.Add((ResultCategory.Information, "Insert", "No rows affected"));
            }

            return executescalar;
        }
        public virtual (Result result, int rows) Update(string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var executenonquery = _sqlrepositorybulk.ExecuteNonQuery(commandtext, commandtype, parameters);

            if (executenonquery.rows == 0)
            {
                executenonquery.result.Messages.Add((ResultCategory.Information, "Update", "No rows affected"));
            }

            return executenonquery;
        }
        public virtual (Result result, int rows) Delete(string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var executenonquery = _sqlrepositorybulk.ExecuteNonQuery(commandtext, commandtype, parameters);

            if (executenonquery.rows == 0)
            {
                executenonquery.result.Messages.Add((ResultCategory.Information, "Delete", "No rows affected"));
            }

            return executenonquery;
        }
    }
}
