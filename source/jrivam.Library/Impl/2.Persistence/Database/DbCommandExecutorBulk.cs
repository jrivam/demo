using jrivam.Library.Interface.Persistence.Database;
using System;
using System.Data;

namespace jrivam.Library.Impl.Persistence.Database
{
    public class DbCommandExecutorBulk : IDbCommandExecutorBulk
    {
        public DbCommandExecutorBulk()
        {
        }

        public virtual (Result result, int rows) ExecuteNonQuery(IDbCommand command)
        {
            try
            {
                command.Connection?.Open();

                int rows = command.ExecuteNonQuery();

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
        public virtual (Result result, object scalar) ExecuteScalar(IDbCommand command)
        {
            try
            {
                command.Connection?.Open();

                object scalar = command.ExecuteScalar();

                command.Connection?.Close();

                return (new Result(), scalar);
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
                { Exception = ex }, null);
            }
        }
    }
}
