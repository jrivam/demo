using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Business
{
    public partial class LogicAsync<T, U> : ILogicAsync<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        public virtual async Task<(Result result, IEnumerable<U> datas)> ListAsync(IQueryData<T, U> query,
            int maxdepth = 1, int top = 0,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            return await query.SelectAsync(maxdepth, top,
                connection,
                commandtimeout).ConfigureAwait(false);
        }

        public virtual async Task<(Result result, U data)> LoadQueryAsync(U data,
            int maxdepth = 1,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn.Value != null)
                {
                    return await data.SelectQueryAsync(maxdepth,
                        connection,
                        commandtimeout).ConfigureAwait(false);
                }

                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Error,
                        Name = $"{this.GetType().Name}.{nameof(LoadQueryAsync)}",
                        Description = $"Id in {data.Description.DbName} cannot be null."
                    }
                    ), default(U));
            }

            return (new Result(
                new ResultMessage()
                {
                    Category = ResultCategory.Error,
                    Name = $"{this.GetType().Name}.{nameof(LoadQueryAsync)}",
                    Description = $"Primary Key column in {data.Description.DbName} not defined."
                }
                    ), default(U));
        }

        public virtual async Task<(Result result, U data)> LoadAsync(U data,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn.Value != null)
                {
                    return await data.SelectAsync(connection,
                        commandtimeout).ConfigureAwait(false);
                }

                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Error,
                        Name = $"{this.GetType().Name}.{nameof(LoadAsync)}",
                        Description = $"Id in {data.Description.DbName} cannot be null."
                    }
                    ), default(U));
            }

            return (new Result(
                new ResultMessage()
                {
                    Category = ResultCategory.Error,
                    Name = $"{this.GetType().Name}.{nameof(LoadAsync)}",
                    Description = $"Primary Key column in {data.Description.DbName} not defined."
                }
                    ), default(U));
        }

        public virtual async Task<(Result result, U data)> SaveAsync(U data,
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn.DbValue != null)
                {
                    var update = await data.UpdateAsync(connection, transaction,
                        commandtimeout).ConfigureAwait(false);

                    return (update.result, update.data);
                }
                else
                {
                    var insert = await data.InsertAsync(connection, transaction,
                        commandtimeout).ConfigureAwait(false);

                    return (insert.result, insert.data);
                }
            }

            return (new Result(
                new ResultMessage()
                {
                    Category = ResultCategory.Error,
                    Name = $"{this.GetType().Name}.{nameof(SaveAsync)}",
                    Description = $"Primary Key column in {data.Description.DbName} not defined."
                }
                    ), default(U));
        }

        public virtual async Task<(Result result, U data)> EraseAsync(U data,
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn?.Value != null)
                {
                    var delete = await data.DeleteAsync(connection, transaction,
                        commandtimeout).ConfigureAwait(false);

                    return (delete.result, delete.data);
                }

                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Error,
                        Name = $"{this.GetType().Name}.{nameof(EraseAsync)}",
                        Description = $"Id in {data.Description.DbName} cannot be null."
                    }
                    ), default(U));
            }

            return (new Result(
                new ResultMessage()
                {
                    Category = ResultCategory.Error,
                    Name = $"{this.GetType().Name}.{nameof(EraseAsync)}",
                    Description = $"Primary Key column in {data.Description.DbName} not defined."
                }
                    ), default(U));
        }
    }
}
