using jrivam.Library.Interface.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace jrivam.Library.Impl.Persistence.Database
{
    public class DbCommandExecutor : IDbCommandExecutor
    {
        public DbCommandExecutor()
        {
        }

        public virtual (Result result, IEnumerable<T> entities) ExecuteQuery<T>(IDbCommand command, Func<T, IDataReader, T> reader)
        {
            try
            {
                var enumeration = new Collection<T>();

                command.Connection?.Open();

                using (var datareader = command.ExecuteReader())
                {
                    while (datareader.Read())
                    {
                        enumeration.Add(reader(Entities.HelperEntities<T>.CreateEntity(), datareader));
                    }

                    datareader.Close();
                }

                command.Connection?.Close();

                return (new Result(), enumeration);
            }
            catch (Exception ex)
            {
                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Exception,
                        Name = nameof(ExecuteQuery),
                        Description = ex.Message
                    })
                { Exception = ex }, default(IEnumerable<T>));
            }
        }

        public virtual (Result result, int rows) ExecuteNonQuery(IDbCommand command)
        {
            try
            {
                command.Connection?.Open();

                var rows = command.ExecuteNonQuery();

                command.Connection?.Close();

                return (new Result(), rows);
            }
            catch (Exception ex)
            {
                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Exception,
                        Name = nameof(ExecuteNonQuery),
                        Description = ex.Message,
                    })
                { Exception = ex }, -1);
            }
        }
        public virtual (Result result, T scalar) ExecuteScalar<T>(IDbCommand command)
        {
            try
            {
                command.Connection?.Open();

                var scalar = command.ExecuteScalar();

                command.Connection?.Close();

                return (new Result(), (T)(scalar == DBNull.Value ? null : Convert.ChangeType(scalar, typeof(T))));
            }
            catch (Exception ex)
            {
                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Exception,
                        Name = nameof(ExecuteScalar),
                        Description = ex.Message
                    })
                { Exception = ex }, default(T));
            }
        }
    }
}
