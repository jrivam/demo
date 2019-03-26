using Library.Interface.Data.Database;
using System;
using System.Collections.Generic;
using System.Data;

namespace Library.Impl.Data.Database
{
    public class DbRepositoryBulk : IDbRepositoryBulk
    {
        public DbRepositoryBulk()
        {
        }

        public virtual (Result result, int rows) ExecuteNonQuery(IDbCommand command)
        {
            var result = new Result() { Success = true };

            try
            {
                command.Connection.Open();

                int rows = command.ExecuteNonQuery();

                command.Connection.Close();

                return (result, rows);
            }
            catch (Exception ex)
            {
                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Exception, "ExecuteNonQuery", $"{ex.Message}{Environment.NewLine}{ex.InnerException}") } }, -1);
            }
        }
        public virtual (Result result, object scalar) ExecuteScalar(IDbCommand command)
        {
            var result = new Result() { Success = true };

            try
            {
                command.Connection.Open();

                object scalar = command.ExecuteScalar();

                command.Connection.Close();

                return (result, scalar);
            }
            catch (Exception ex)
            {
                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Exception, "ExecuteScalar", $"{ex.Message}{Environment.NewLine}{ex.InnerException}") } }, null);
            }
        }
    }
}
