using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Persistence
{
    public class RepositoryEF6 : IRepositoryAsync
    {
        protected readonly DbContext _context;

        public RepositoryEF6(DbContext context)
        {
            _context = context;
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
                var sqlquery = await _context.Database.SqlQuery<T>(commandtext, parameters?.Select(x => x.Value)).ToListAsync().ConfigureAwait(false);

                return (new Result(), sqlquery);
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
                { Exception = ex }, null);
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
                var executesqlcommand = await _context.Database.ExecuteSqlCommandAsync(commandtext, parameters?.Select(x => x.Value)).ConfigureAwait(false);

                return (new Result(), executesqlcommand);
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
                var sqlquery = await _context.Database.SqlQuery<T>(commandtext, parameters?.Select(x => x.Value)).SingleAsync().ConfigureAwait(false);

                return (new Result(), sqlquery);
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
