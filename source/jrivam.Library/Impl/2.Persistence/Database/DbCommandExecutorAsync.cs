using jrivam.Library.Interface.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Persistence.Database
{
    public partial class DbCommandExecutorAsync : DbCommandExecutor, IDbCommandExecutorAsync
    {
        public DbCommandExecutorAsync()
            : base()
        {
        }

        public virtual async Task<(Result result, IEnumerable<T> entities)> ExecuteQueryAsync<T>(IDbCommand command, Func<T, IDataReader, T> reader, CommandBehavior commandbehavior = CommandBehavior.Default)
        {
            try
            {
                var enumeration = new Collection<T>();

                if (command.Connection.State != ConnectionState.Open)
                {
                    await ((DbConnection)command.Connection).OpenAsync().ConfigureAwait(false);
                }

                using (var datareader = await ((DbCommand)command).ExecuteReaderAsync(commandbehavior).ConfigureAwait(false))
                {
                    while (await datareader.ReadAsync().ConfigureAwait(false))
                    {
                        enumeration.Add(reader(Entities.HelperEntities<T>.CreateEntity(), datareader));
                    }

                    datareader.Close();
                }

                return (new Result(), enumeration);
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

        public virtual async Task<(Result result, int rows)> ExecuteNonQueryAsync(IDbCommand command)
        {
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    await ((DbConnection)command.Connection).OpenAsync().ConfigureAwait(false);
                }

                var rows = await ((DbCommand)command).ExecuteNonQueryAsync().ConfigureAwait(false);

                return (new Result(), rows);
            }
            catch (Exception ex)
            {
                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Exception,
                        Name = $"{this.GetType().Name}.{nameof(ExecuteNonQueryAsync)}",
                        Description = ex.Message,
                    })
                { Exception = ex }, -1);
            }
        }

        public virtual async Task<(Result result, T scalar)> ExecuteScalarAsync<T>(IDbCommand command)
        {
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    await ((DbConnection)command.Connection).OpenAsync().ConfigureAwait(false);
                }

                var scalar = await ((DbCommand)command).ExecuteScalarAsync().ConfigureAwait(false);

                return (new Result(), (T)(scalar == DBNull.Value ? null : Convert.ChangeType(scalar, typeof(T))));
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
