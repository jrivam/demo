using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace jrivam.Library.Impl.Business
{
    public class Logic<T, U> : ILogic<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        public Logic()
        {
        }

        public virtual (Result result, U data) Load(U data, 
            bool usedbcommand = false, 
            int? commandtimeout = null,
            IDbConnection connection = null)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn.Value != null)
                {
                    return data.Select(usedbcommand, commandtimeout,
                        connection);
                }

                return (new Result(
                    new ResultMessage()
                        {
                            Category = ResultCategory.Error,
                            Name = $"{this.GetType().Name}.{nameof(Load)}",
                            Description =  $"Id in {data.Description.DbName} cannot be null."
                        }
                    ), default(U));
            }

            return (new Result(
                new ResultMessage()
                        {
                            Category = ResultCategory.Error,
                            Name = $"{this.GetType().Name}.{nameof(Load)}",
                            Description =  $"Primary Key column in {data.Description.DbName} not defined."
                        }
                    ), default(U));
        }
        public virtual (Result result, U data) LoadQuery(U data, 
            int? commandtimeout = null,
            int maxdepth = 1, 
            IDbConnection connection = null)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn.Value != null)
                {
                    return data.SelectQuery(commandtimeout, 
                        maxdepth,
                        connection);
                }

                return (new Result(
                    new ResultMessage()
                        {
                            Category = ResultCategory.Error,
                            Name = $"{this.GetType().Name}.{nameof(LoadQuery)}",
                            Description =  $"Id in {data.Description.DbName} cannot be null."
                        }
                    ), default(U));
            }

            return (new Result(
                new ResultMessage()
                        {
                            Category = ResultCategory.Error,
                            Name = $"{this.GetType().Name}.{nameof(LoadQuery)}",
                            Description = $"Primary Key column in {data.Description.DbName} not defined."
                        }
                    ), default(U));
        }

        public virtual (Result result, IEnumerable<U> datas) List(IQueryData<T, U> query,
            int? commandtimeout = null, 
            int maxdepth = 1, int top = 0,
            IDbConnection connection = null)
        {
            return query.Select(commandtimeout, 
                maxdepth, top,
                connection);
        }

        public virtual (Result result, U data) Save(U data, 
            bool useinsertdbcommand = false, bool useupdatedbcommand = false,
            int? commandtimeout = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn.DbValue != null)
                {
                    var update = data.Update(useupdatedbcommand,
                        commandtimeout,
                        connection, transaction);

                    return (update.result, update.data);
                }
                else
                {
                    var insert = data.Insert(useinsertdbcommand,
                        commandtimeout,
                        connection, transaction);

                    return (insert.result, insert.data);
                }
            }

            return (new Result(
                new ResultMessage()
                        {
                            Category = ResultCategory.Error,
                            Name = $"{this.GetType().Name}.{nameof(Save)}",
                            Description = $"Primary Key column in {data.Description.DbName} not defined."
                        }
                    ), default(U));
        }
        public virtual (Result result, U data) Erase(U data, 
            bool usedbcommand = false,
            int? commandtimeout = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn?.Value != null)
                {
                    var delete = data.Delete(usedbcommand,
                        commandtimeout,
                        connection, transaction);

                    return (delete.result, delete.data);
                }

                return (new Result(
                    new ResultMessage()
                        {
                            Category = ResultCategory.Error,
                            Name = $"{this.GetType().Name}.{nameof(Erase)}",
                            Description = $"Id in {data.Description.DbName} cannot be null."
                        }
                    ), default(U));                
            }

            return (new Result(
                new ResultMessage()
                        {
                            Category = ResultCategory.Error,
                            Name = $"{this.GetType().Name}.{nameof(Erase)}",
                            Description = $"Primary Key column in {data.Description.DbName} not defined."
                        }
                    ), default(U));
        }
    }
}
