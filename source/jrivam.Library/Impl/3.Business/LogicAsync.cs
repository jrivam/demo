﻿using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Business
{
    public class Logic<T, U> : ILogic<T, U>
        where T : IEntity
        where U : ITableData<T, U>, ITableDataMethodsAuto<T, U>, ITableDataMethodsAutoAsync<T, U>, ITableDataMethodsCommand<T, U>, ITableDataMethodsCommandAsync<T, U>
    {
        public Logic()
        {
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
        public virtual async Task<(Result result, IEnumerable<U> datas)> ListAsync(IQueryData<T, U> query,
            int? commandtimeout = null,
            int maxdepth = 1, int top = 0,
            IDbConnection connection = null)
        {
            return await query.SelectAsync(commandtimeout,
                maxdepth, top,
                connection);
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
                        Description = $"Id in {data.Description.DbName} cannot be null."
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
        public virtual async Task<(Result result, U data)> LoadQueryAsync(U data,
            int? commandtimeout = null,
            int maxdepth = 1,
            IDbConnection connection = null)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn.Value != null)
                {
                    return await data.SelectQueryAsync(commandtimeout,
                        maxdepth,
                        connection);
                }

                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Error,
                        Name = $"{this.GetType().Name}.{nameof(LoadQuery)}",
                        Description = $"Id in {data.Description.DbName} cannot be null."
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

        public virtual (Result result, U data) Load(U data, 
            int? commandtimeout = null,
            IDbConnection connection = null)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn.Value != null)
                {
                    return data.Select(commandtimeout,
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
        public virtual async Task<(Result result, U data)> LoadAsync(U data,
            int? commandtimeout = null,
            IDbConnection connection = null)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn.Value != null)
                {
                    return await data.SelectAsync(commandtimeout,
                        connection);
                }

                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Error,
                        Name = $"{this.GetType().Name}.{nameof(Load)}",
                        Description = $"Id in {data.Description.DbName} cannot be null."
                    }
                    ), default(U));
            }

            return (new Result(
                new ResultMessage()
                {
                    Category = ResultCategory.Error,
                    Name = $"{this.GetType().Name}.{nameof(Load)}",
                    Description = $"Primary Key column in {data.Description.DbName} not defined."
                }
                    ), default(U));
        }
        public virtual (Result result, U data) LoadCommand(U data,
            IDbConnection connection = null)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn.Value != null)
                {
                    return data.SelectCommand(connection);
                }

                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Error,
                        Name = $"{this.GetType().Name}.{nameof(Load)}",
                        Description = $"Id in {data.Description.DbName} cannot be null."
                    }
                    ), default(U));
            }

            return (new Result(
                new ResultMessage()
                {
                    Category = ResultCategory.Error,
                    Name = $"{this.GetType().Name}.{nameof(Load)}",
                    Description = $"Primary Key column in {data.Description.DbName} not defined."
                }
                    ), default(U));
        }
        public virtual async Task<(Result result, U data)> LoadCommandAsync(U data,
            IDbConnection connection = null)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn.Value != null)
                {
                    return await data.SelectCommandAsync(connection);
                }

                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Error,
                        Name = $"{this.GetType().Name}.{nameof(Load)}",
                        Description = $"Id in {data.Description.DbName} cannot be null."
                    }
                    ), default(U));
            }

            return (new Result(
                new ResultMessage()
                {
                    Category = ResultCategory.Error,
                    Name = $"{this.GetType().Name}.{nameof(Load)}",
                    Description = $"Primary Key column in {data.Description.DbName} not defined."
                }
                    ), default(U));
        }

        public virtual (Result result, U data) Save(U data, 
            int? commandtimeout = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn.DbValue != null)
                {
                    var update = data.Update(commandtimeout,
                        connection, transaction);

                    return (update.result, update.data);
                }
                else
                {
                    var insert = data.Insert(commandtimeout,
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
        public virtual async Task<(Result result, U data)> SaveAsync(U data,
            int? commandtimeout = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn.DbValue != null)
                {
                    var update = await data.UpdateAsync(commandtimeout,
                        connection, transaction);

                    return (update.result, update.data);
                }
                else
                {
                    var insert = await data.InsertAsync(commandtimeout,
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
        public virtual (Result result, U data) SaveCommand(U data,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn.DbValue != null)
                {
                    var update = data.UpdateCommand(connection, transaction);

                    return (update.result, update.data);
                }
                else
                {
                    var insert = data.InsertCommand(connection, transaction);

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
        public virtual async Task<(Result result, U data)> SaveCommandAsync(U data,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn.DbValue != null)
                {
                    var update = await data.UpdateCommandAsync(connection, transaction);

                    return (update.result, update.data);
                }
                else
                {
                    var insert = await data.InsertCommandAsync(connection, transaction);

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
            int? commandtimeout = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn?.Value != null)
                {
                    var delete = data.Delete(commandtimeout,
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
        public virtual async Task<(Result result, U data)> EraseAsync(U data,
            int? commandtimeout = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn?.Value != null)
                {
                    var delete = await data.DeleteAsync(commandtimeout,
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
        public virtual (Result result, U data) EraseCommand(U data,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn?.Value != null)
                {
                    var delete = data.DeleteCommand(connection, transaction);

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
        public virtual async Task<(Result result, U data)> EraseCommandAsync(U data,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var primarykeycolumn = data.Columns.FirstOrDefault(x => x.IsPrimaryKey);
            if (primarykeycolumn != null)
            {
                if (primarykeycolumn?.Value != null)
                {
                    var delete = await data.DeleteCommandAsync(connection, transaction);

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
