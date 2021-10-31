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
    public partial class RepositoryAsync : IRepositoryAsync
    {
        protected readonly ConnectionStringSettings _connectionstringsettings;

        protected readonly ISqlCreator _sqlcreator;

        protected readonly IDbCommandExecutorAsync _dbcommandexecutorasync;
        protected readonly IEntityReader _entityreader;

        public RepositoryAsync(
            ConnectionStringSettings connectionstringsettings,
            ISqlCreator sqlcreator,
            IDbCommandExecutorAsync dbcommandexecutorasync, IEntityReader entityreader)
        {
            _connectionstringsettings = connectionstringsettings;

            _sqlcreator = sqlcreator;

            _dbcommandexecutorasync = dbcommandexecutorasync;
            _entityreader = entityreader;
        }

        protected IDbCommand GetCommand(
            string commandtext, CommandType commandtype = CommandType.Text, 
            ISqlParameter[] parameters = null,
            IDbConnection connection = null, IDbTransaction transaction = null,
            int commandtimeout = 30)
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

        public virtual async Task<(Result result, IEnumerable<T> entities)> ExecuteQueryAsync<T>(
            string commandtext, CommandType commandtype = CommandType.Text,
            ISqlParameter[] parameters = null,
            int maxdepth = 1,
            IDbConnection connection = null,
            int commandtimeout = 30)
        {
            var closeConnection = (connection == null);

            var executequery = await _dbcommandexecutorasync.ExecuteQueryAsync<T>(
                GetCommand(commandtext, commandtype,
                parameters,
                connection,
                commandtimeout: commandtimeout),
                (x, y) => _entityreader.Read<T>(x, y, new List<string>(), maxdepth, 0)).ConfigureAwait(false);

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

        public virtual async Task<(Result result, int rows)> ExecuteNonQueryAsync(
            string commandtext, CommandType commandtype = CommandType.Text,
            ISqlParameter[] parameters = null,
            IDbConnection connection = null, IDbTransaction transaction = null,
            int commandtimeout = 30)
        {
            var executenonquery = await _dbcommandexecutorasync.ExecuteNonQueryAsync(
                GetCommand(commandtext, commandtype,
                parameters,
                connection, transaction,
                commandtimeout)).ConfigureAwait(false);
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

        public virtual async Task<(Result result, T scalar)> ExecuteScalarAsync<T>(
            string commandtext, CommandType commandtype = CommandType.Text,
            ISqlParameter[] parameters = null,
            IDbConnection connection = null, IDbTransaction transaction = null,
            int commandtimeout = 30)
        {
            var executescalar = await _dbcommandexecutorasync.ExecuteScalarAsync<T>(
                GetCommand(commandtext, commandtype,
                parameters,
                connection, transaction,
                commandtimeout)).ConfigureAwait(false);

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
