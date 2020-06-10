using Autofac;
using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Impl.Persistence.Sql.Factory;
using jrivam.Library.Interface.Entities.Reader;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Database;
using jrivam.Library.Interface.Persistence.Sql.Builder;
using jrivam.Library.Interface.Persistence.Sql.Database;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace jrivam.Library.Impl.Persistence
{
    public class Repository : IRepository
    {
        protected readonly ConnectionStringSettings _connectionstringsettings;

        protected readonly ISqlCreator _creator;

        protected readonly IDbCommandExecutor _dbcommandexecutor;
        protected readonly IDbCommandExecutorBulk _dbcommandexecutorbulk;

        public Repository(ConnectionStringSettings connectionstringsettings,
            ISqlCreator creator,
            IDbCommandExecutor dbcommandexecutor, IDbCommandExecutorBulk dbcommandexecutorbulk)
        {
            _connectionstringsettings = connectionstringsettings;

            _creator = creator;

            _dbcommandexecutor = dbcommandexecutor;
            _dbcommandexecutorbulk = dbcommandexecutorbulk;
        }

        public virtual (Result result, IEnumerable<T> entities) ExecuteQuery<T>(SqlCommand sqlcommand,
            int maxdepth = 1, ICollection<T> entities = null)
        {
            return ExecuteQuery(sqlcommand.Text, sqlcommand.Type, sqlcommand.Parameters, maxdepth, entities);
        }
        public virtual (Result result, IEnumerable<T> entities) ExecuteQuery<T>(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, int maxdepth = 1, ICollection<T> entities = null)
        {
            var executequery = _dbcommandexecutor.ExecuteQuery(_creator.GetCommand(_connectionstringsettings.ProviderName, _connectionstringsettings.ConnectionString, commandtext, commandtype, parameters),
                AutofacConfiguration.Container.Resolve<IEntityReader>(new TypedParameter(typeof(ISqlSyntaxSign), SqlSyntaxSignFactory.Create(_connectionstringsettings.ProviderName))),
                maxdepth, entities);

            if ((executequery.entities?.Count() ?? 0) == 0)
            {
                executequery.result.Messages.Add((ResultCategory.Information, nameof(ExecuteQuery), "No rows found"));
            }

            return executequery;
        }

        public virtual (Result result, int rows) ExecuteNonQuery(SqlCommand sqlcommand)
        {
            return ExecuteNonQuery(sqlcommand.Text, sqlcommand.Type, sqlcommand.Parameters);
        }
        public virtual (Result result, int rows) ExecuteNonQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null)
        {
            var executenonquery = _dbcommandexecutorbulk.ExecuteNonQuery(_creator.GetCommand(_connectionstringsettings.ProviderName, _connectionstringsettings.ConnectionString, 
                commandtext, commandtype, parameters));

            if (executenonquery.rows == 0)
            {
                executenonquery.result.Messages.Add((ResultCategory.Information, nameof(ExecuteNonQuery), "No rows affected"));
            }

            return executenonquery;
        }

        public virtual (Result result, object scalar) ExecuteScalar(SqlCommand sqlcommand)
        {
            return ExecuteScalar(sqlcommand.Text, sqlcommand.Type, sqlcommand.Parameters);
        }
        public virtual (Result result, object scalar) ExecuteScalar(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null)
        {
            var executescalar = _dbcommandexecutorbulk.ExecuteScalar(_creator.GetCommand(_connectionstringsettings.ProviderName, _connectionstringsettings.ConnectionString, 
                commandtext, commandtype, parameters));

            if (executescalar.scalar == null)
            {
                executescalar.result.Messages.Add((ResultCategory.Information, nameof(ExecuteScalar), "No rows affected"));
            }

            return executescalar;
        }
    }
}
