using jrivam.Library.Interface.Entities.Reader;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Database;
using jrivam.Library.Interface.Persistence.Sql;
using jrivam.Library.Interface.Persistence.Sql.Database;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Persistence
{
    public class Repository : IRepository
    {
        protected readonly ConnectionStringSettings _connectionstringsettings;

        protected readonly ISqlCreator _sqlcreator;

        protected readonly IDbCommandExecutor _dbcommandexecutor;
        protected readonly IEntityReader _entityreader;

        public Repository(
            ConnectionStringSettings connectionstringsettings,
            ISqlCreator sqlcreator,
            IDbCommandExecutor dbcommandexecutor, IEntityReader entityreader)
        {
            _connectionstringsettings = connectionstringsettings;

            _sqlcreator = sqlcreator;

            _dbcommandexecutor = dbcommandexecutor;
            _entityreader = entityreader;
        }

        protected IDbCommand GetCommand(
            string commandtext, CommandType commandtype = CommandType.Text, int commandtimeout = 30,
            ISqlParameter[] parameters = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            if (connection == null)
            {
                connection = _sqlcreator.GetConnection(
                        _connectionstringsettings.ProviderName,
                        _connectionstringsettings.ConnectionString);
            }

            var command = _sqlcreator.GetCommand(_connectionstringsettings.ProviderName,
                                        commandtext, commandtype, commandtimeout,
                                        parameters);

            command.Connection = connection;
            command.Transaction = transaction;

            return command;
        }

        public virtual (Result result, IEnumerable<T> entities) ExecuteQuery<T>(
            string commandtext, CommandType commandtype = CommandType.Text, int commandtimeout = 30,
            ISqlParameter[] parameters = null,
            int maxdepth = 1,
            IDbConnection connection = null)
        {
            var closeConnection = (connection == null);

            var executequery = _dbcommandexecutor.ExecuteQuery<T>(
                    GetCommand(commandtext, commandtype, commandtimeout,
                    parameters,
                    connection),    
                (x, y) => _entityreader.Read<T>(x, y, new List<string>(), maxdepth, 0));

            if (closeConnection)
            {
                connection?.Close();
            }

            if (executequery.result.Success && executequery.entities?.Count() == 0)
            {
                executequery.result.SetMessage(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Information,
                        Name = $"{this.GetType().Name}.{nameof(ExecuteQuery)}",
                        Description = "No rows found."
                    }
                );
            }

            return executequery;
        }
        public virtual async Task<(Result result, IEnumerable<T> entities)> ExecuteQueryAsync<T>(
            string commandtext, CommandType commandtype = CommandType.Text, int commandtimeout = 30,
            ISqlParameter[] parameters = null,
            int maxdepth = 1,
            IDbConnection connection = null)
        {
            var closeConnection = (connection == null);

            var executequery = await _dbcommandexecutor.ExecuteQueryAsync<T>(
                GetCommand(commandtext, commandtype, commandtimeout,
                parameters,
                connection),
                (x, y) => _entityreader.Read<T>(x, y, new List<string>(), maxdepth, 0));

            if (closeConnection)
            {
                connection?.Close();
            }

            if (executequery.result.Success && executequery.entities?.Count() == 0)
            {
                executequery.result.SetMessage(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Information,
                        Name = $"{this.GetType().Name}.{nameof(ExecuteQueryAsync)}",
                        Description = "No rows found."
                    }
                );
            }

            return executequery;
        }

        public virtual (Result result, int rows) ExecuteNonQuery(
            string commandtext, CommandType commandtype = CommandType.Text, int commandtimeout = 30,
            ISqlParameter[] parameters = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var executenonquery = _dbcommandexecutor.ExecuteNonQuery(
                GetCommand(commandtext, commandtype, commandtimeout,
                parameters,
                connection, transaction));

            if (transaction == null)
            {
                connection?.Close();
            }

            if (executenonquery.result.Success && executenonquery.rows == 0)
            {
                executenonquery.result.SetMessage(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Information,
                        Name = $"{this.GetType().Name}.{nameof(ExecuteNonQuery)}",
                        Description = "No rows affected."
                    }
                );
            }

            return executenonquery;
        }
        public virtual async Task<(Result result, int rows)> ExecuteNonQueryAsync(
            string commandtext, CommandType commandtype = CommandType.Text, int commandtimeout = 30,
            ISqlParameter[] parameters = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var executenonquery = await _dbcommandexecutor.ExecuteNonQueryAsync(
                GetCommand(commandtext, commandtype, commandtimeout,
                parameters,
                connection, transaction));
            if (transaction == null)
            {
                connection?.Close();
            }

            if (executenonquery.result.Success && executenonquery.rows == 0)
            {
                executenonquery.result.SetMessage(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Information,
                        Name = $"{this.GetType().Name}.{nameof(ExecuteNonQueryAsync)}",
                        Description = "No rows affected."
                    }
                );
            }

            return executenonquery;
        }

        public virtual (Result result, T scalar) ExecuteScalar<T>(
            string commandtext, CommandType commandtype = CommandType.Text, int commandtimeout = 30,
            ISqlParameter[] parameters = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {           
            var executescalar = _dbcommandexecutor.ExecuteScalar<T>(
                GetCommand(commandtext, commandtype, commandtimeout,
                parameters,
                connection, transaction));

            if (transaction == null)
            {
                connection?.Close();
            }

            if (executescalar.result.Success && executescalar.scalar == null)
            {
                executescalar.result.SetMessage(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Information,
                        Name = $"{this.GetType().Name}.{nameof(ExecuteScalar)}",
                        Description = "No rows affected."
                    }
                );
            }

            return executescalar;
        }
        public virtual async Task<(Result result, T scalar)> ExecuteScalarAsync<T>(
            string commandtext, CommandType commandtype = CommandType.Text, int commandtimeout = 30,
            ISqlParameter[] parameters = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var executescalar = await _dbcommandexecutor.ExecuteScalarAsync<T>(
                GetCommand(commandtext, commandtype, commandtimeout,
                parameters,
                connection, transaction));

            if (transaction == null)
            {
                connection?.Close();
            }

            if (executescalar.result.Success && executescalar.scalar == null)
            {
                executescalar.result.SetMessage(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Information,
                        Name = $"{this.GetType().Name}.{nameof(ExecuteScalarAsync)}",
                        Description = "No rows affected."
                    }
                );
            }

            return executescalar;
        }
    }
}
