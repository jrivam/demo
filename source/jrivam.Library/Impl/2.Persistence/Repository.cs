using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Database;
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

        protected readonly ISqlCreator _sqlcreator;

        protected readonly IDbCommandExecutor _dbcommandexecutor;
        protected readonly IDbCommandExecutorBulk _dbcommandexecutorbulk;

        public Repository(ConnectionStringSettings connectionstringsettings,
            ISqlCreator sqlcreator,
            IDbCommandExecutor dbcommandexecutor, IDbCommandExecutorBulk dbcommandexecutorbulk)
        {
            _connectionstringsettings = connectionstringsettings;

            _sqlcreator = sqlcreator;

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
            var executequery = _dbcommandexecutor.ExecuteQuery(_sqlcreator.GetCommand(_connectionstringsettings.ProviderName, 
                    commandtext, commandtype, 
                    parameters, 
                    _connectionstringsettings.ConnectionString),
                maxdepth, entities);

            if(executequery.result.Success && executequery.entities?.Count() == 0)
            {
                executequery.result.SetMessage(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Information,
                        Name = nameof(ExecuteQuery),
                        Description = "No rows found."
                    }
                );
            }

            return executequery;
        }

        public virtual (Result result, int rows) ExecuteNonQuery(SqlCommand sqlcommand)
        {
            return ExecuteNonQuery(sqlcommand.Text, sqlcommand.Type, sqlcommand.Parameters);
        }
        public virtual (Result result, int rows) ExecuteNonQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null)
        {
            var executenonquery = _dbcommandexecutorbulk.ExecuteNonQuery(_sqlcreator.GetCommand(_connectionstringsettings.ProviderName,  
                    commandtext, commandtype, 
                    parameters,
                    _connectionstringsettings.ConnectionString));

            if (executenonquery.result.Success && executenonquery.rows == 0)
            {
                executenonquery.result.SetMessage(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Information,
                        Name = nameof(ExecuteNonQuery),
                        Description = "No rows affected."
                    }
                );
            }

            return executenonquery;
        }

        public virtual (Result result, object scalar) ExecuteScalar(SqlCommand sqlcommand)
        {
            return ExecuteScalar(sqlcommand.Text, sqlcommand.Type, sqlcommand.Parameters);
        }
        public virtual (Result result, object scalar) ExecuteScalar(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null)
        {
            var executescalar = _dbcommandexecutorbulk.ExecuteScalar(_sqlcreator.GetCommand(_connectionstringsettings.ProviderName,  
                    commandtext, commandtype, 
                    parameters,
                    _connectionstringsettings.ConnectionString));

            if (executescalar.result.Success && executescalar.scalar == null)
            {
                executescalar.result.SetMessage(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Information,
                        Name = nameof(ExecuteScalar),
                        Description = "No rows affected."
                    }
                );
            }

            return executescalar;
        }
    }
}
