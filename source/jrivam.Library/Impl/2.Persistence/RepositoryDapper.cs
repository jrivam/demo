using Dapper;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Sql;
using jrivam.Library.Interface.Persistence.Sql.Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Persistence
{
    public class RepositoryDapper : IRepositoryAsync
    {
        protected readonly ConnectionStringSettings _connectionstringsettings;

        protected readonly ISqlCreator _sqlcreator;

        public RepositoryDapper(
            ConnectionStringSettings connectionstringsettings,
            ISqlCreator sqlcreator)
        {
            _connectionstringsettings = connectionstringsettings;

            _sqlcreator = sqlcreator;
        }

        public virtual async Task<(Result result, IEnumerable<T> entities)> ExecuteQueryAsync<T>(
            string commandtext, CommandType commandtype = CommandType.Text, 
            ISqlParameter[] parameters = null,
            int maxdepht = 1,
            IDbConnection connection = null,
            int commandtimeout = 30)
        {
            try
            {
                var closeConnection = (connection == null);

                if (connection == null)
                {
                    connection = _sqlcreator.GetConnection(
                        _connectionstringsettings.ProviderName,
                        _connectionstringsettings.ConnectionString);
                }

                var p = new DynamicParameters();

                foreach (var parameter in parameters)
                {
                    p.Add(parameter.Name, parameter.Value);
                }

                if (connection.State != ConnectionState.Open)
                {
                    await ((DbConnection)connection).OpenAsync().ConfigureAwait(false);
                }

                var query = await connection.QueryAsync<T>(commandtext, p, commandTimeout: commandtimeout).ConfigureAwait(false);

                if (closeConnection)
                {
                    connection?.Close();
                }

                return (new Result(), query);

            }
            catch (Exception ex)
            {
                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Exception,
                        Name = $"{this.GetType().Name}.{nameof(ExecuteQueryAsync)}",
                        Description = ex.Message
                    })
                { Exception = ex }, default(IEnumerable<T>));
            }
        }

        public virtual async Task<(Result result, int rows)> ExecuteNonQueryAsync(
            string commandtext, CommandType commandtype = CommandType.Text, 
            ISqlParameter[] parameters = null,
            IDbConnection connection = null, IDbTransaction transaction = null,
            int commandtimeout = 30)
        {
            try
            {
                if (connection == null)
                {
                    connection = _sqlcreator.GetConnection(
                        _connectionstringsettings.ProviderName,
                        _connectionstringsettings.ConnectionString);
                }

                var p = new DynamicParameters();

                foreach (var parameter in parameters)
                {
                    p.Add(parameter.Name, parameter.Value);
                }

                if (connection.State != ConnectionState.Open)
                {
                    await ((DbConnection)connection).OpenAsync().ConfigureAwait(false);
                }

                var query = await connection.QueryAsync<int>(commandtext, p, transaction, commandTimeout: commandtimeout).ConfigureAwait(false);

                if (transaction == null)
                {
                    connection?.Close();
                }

                return (new Result(), query.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Exception,
                        Name = $"{this.GetType().Name}.{nameof(ExecuteNonQueryAsync)}",
                        Description = ex.Message
                    })
                { Exception = ex }, -1);
            }
        }

        public virtual async Task<(Result result, T scalar)> ExecuteScalarAsync<T>(
            string commandtext, CommandType commandtype = CommandType.Text, 
            ISqlParameter[] parameters = null,
            IDbConnection connection = null, IDbTransaction transaction = null,
            int commandtimeout = 30)
        {
            try
            {
                if (connection == null)
                {
                    connection = _sqlcreator.GetConnection(
                        _connectionstringsettings.ProviderName,
                        _connectionstringsettings.ConnectionString);
                }

                var p = new DynamicParameters();

                foreach (var parameter in parameters)
                {
                    p.Add(parameter.Name, parameter.Value);
                }

                if (connection.State != ConnectionState.Open)
                {
                    await ((DbConnection)connection).OpenAsync().ConfigureAwait(false);
                }

                var execute = await connection.ExecuteAsync(commandtext, p, transaction, commandTimeout: commandtimeout).ConfigureAwait(false);

                if (transaction == null)
                {
                    connection?.Close();
                }

                return (new Result(), (T)Convert.ChangeType(execute, typeof(T)));
            }
            catch (Exception ex)
            {
                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Exception,
                        Name = $"{this.GetType().Name}.{nameof(ExecuteScalarAsync)}",
                        Description = ex.Message
                    })
                { Exception = ex }, default(T));
            }
        }
    }
}
