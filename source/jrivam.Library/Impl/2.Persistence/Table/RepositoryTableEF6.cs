using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Mapper;
using jrivam.Library.Interface.Persistence.Sql;
using jrivam.Library.Interface.Persistence.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Persistence.Table
{
    public class RepositoryTableEF6<T, U> : IRepositoryTableAsync<T, U>
        where T : class, IEntity
        where U : class, ITableData<T, U>
    {
        protected readonly DbContext _context;

        protected readonly IDataMapper _datamapper;

        public RepositoryTableEF6(DbContext context, IDataMapper datamapper)
        {
            _context = context;

            _datamapper = datamapper;
        }

        public async Task<(Result result, U data)> SelectAsync(U data,
            IDbConnection connection = null,
            int commandtimeout = 30)
        {
            try
            {
                var find = await _context.Set<T>().FindAsync(data.Columns.Where(c => c.IsPrimaryKey).Select(x => x.Value).ToArray()).ConfigureAwait(false);

                if (find != null)
                {
                    data.Entity = find;

                    _datamapper.Map<T, U>(data, 1);
                }

                return (new Result(), data);
            }
            catch (Exception ex)
            {
                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Exception,
                        Name = $"{this.GetType().Name}.{nameof(SelectAsync)}",
                        Description = ex.Message
                    })
                { Exception = ex }, null);
            }
        }
        public async Task<(Result result, U data)> SelectAsync(U data,
            string commandtext, CommandType commandtype = CommandType.StoredProcedure,
            IList<ISqlParameter> parameters = null,
            IDbConnection connection = null, 
            int commandtimeout = 30)
        {
            foreach (var c in data.Columns.Where(c => c.IsPrimaryKey))
            {
                var parameter = Helper.GetParameter($"{c.Table.Description.Name}_{c.Description.Name}", c.Type, c.Value, ParameterDirection.Input);
                if (parameters.FirstOrDefault(x => x.Name == parameter.Name) == null)
                {
                    parameters.Add(parameter);
                }
            }

            try
            {
                var sqlquery = await _context.Database.SqlQuery<T>($"{commandtext} {string.Join(", ", parameters?.Select(x => x.Name))}", parameters?.Select(x => x.Value).ToArray()).ToListAsync().ConfigureAwait(false);
                if (sqlquery.Count() > 0)
                {
                    data.Entity = sqlquery.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Exception,
                        Name = $"{this.GetType().Name}.{nameof(SelectAsync)}",
                        Description = ex.Message
                    })
                { Exception = ex }, null);
            }

            return (new Result(), data);
        }

        public async Task<(Result result, U data)> InsertAsync(U data,
            IDbConnection connection = null, IDbTransaction transaction = null,
            int commandtimeout = 30)
        {
            try
            {
                _context.Set<T>().Add(data.Entity);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                _datamapper.Map<T, U>(data, 1);

                return (new Result(), data);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        public async Task<(Result result, U data)> InsertAsync(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure,
            IList<ISqlParameter> parameters = null,
            IDbConnection connection = null, IDbTransaction transaction = null, 
            int commandtimeout = 30)
        {
            foreach (var c in data.Columns.Where(c => !c.IsIdentity))
            {
                var parameter = Helper.GetParameter($"{c.Table.Description.Name}_{c.Description.Name}", c.Type, c.Value, ParameterDirection.Input);

                var commandparameter = parameters?.FirstOrDefault(x => x.Name == parameter.Name);
                if (commandparameter == null)
                {
                    parameters.Add(parameter);
                }
                else
                {
                    commandparameter.Value = c.Value;
                }
            }

            try
            {
                var executesqlcommand = await _context.Database.ExecuteSqlCommandAsync($"{commandtext} {string.Join(", ", parameters.Select(x => x.Name))}", parameters.Select(x => x.Value).ToArray()).ConfigureAwait(false);
                if (executesqlcommand > 0)
                {
                    data.Entity.Id = executesqlcommand;
                }
            }
            catch (Exception ex)
            {
                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Exception,
                        Name = $"{this.GetType().Name}.{nameof(InsertAsync)}",
                        Description = ex.Message
                    })
                { Exception = ex }, null);
            }

            return (new Result(), data);
        }

        public async Task<(Result result, U data)> UpdateAsync(U data,
            IDbConnection connection = null, IDbTransaction transaction = null,
            int commandtimeout = 30)
        {
            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);

                _datamapper.Map<T, U>(data, 1);

                return (new Result(), data);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        public async Task<(Result result, U data)> UpdateAsync(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure,
            IList<ISqlParameter> parameters = null,
            IDbConnection connection = null, IDbTransaction transaction = null, 
            int commandtimeout = 30)
        {
            foreach (var c in data.Columns)
            {
                var parameter = Helper.GetParameter($"{c.Table.Description.Name}_{c.Description.Name}", c.Type, c.Value, ParameterDirection.Input);

                var commandparameter = parameters?.FirstOrDefault(x => x.Name == parameter.Name);
                if (commandparameter == null)
                {
                    parameters.Add(parameter);
                }
                else
                {
                    commandparameter.Value = c.Value;
                }
            }

            try
            {
                var executesqlcommand = await _context.Database.ExecuteSqlCommandAsync($"{commandtext} {string.Join(", ", parameters.Select(x => x.Name))}", parameters.Select(x => x.Value).ToArray()).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Exception,
                        Name = $"{this.GetType().Name}.{nameof(UpdateAsync)}",
                        Description = ex.Message
                    })
                { Exception = ex }, null);
            }

            return (new Result(), data);
        }

        public async Task<(Result result, U data)> DeleteAsync(U data,
            IDbConnection connection = null, IDbTransaction transaction = null,
            int commandtimeout = 30)
        {
            try
            {
                _context.Set<T>().Remove(data.Entity);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return (new Result(), data);
            }
            catch (Exception ex)
            {
                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Exception,
                        Name = $"{this.GetType().Name}.{nameof(DeleteAsync)}",
                        Description = ex.Message
                    })
                { Exception = ex }, null);
            }
        }
        public async Task<(Result result, U data)> DeleteAsync(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure,
            IList<ISqlParameter> parameters = null,
            IDbConnection connection = null, IDbTransaction transaction = null,
            int commandtimeout = 30)
        {
            foreach (var c in data.Columns.Where(c => c.IsPrimaryKey))
            {
                var parameter = Helper.GetParameter($"{c.Table.Description.Name}_{c.Description.Name}", c.Type, c.Value, ParameterDirection.Input);

                var commandparameter = parameters?.FirstOrDefault(x => x.Name == parameter.Name);
                if (commandparameter == null)
                {
                    parameters.Add(parameter);
                }
                else
                {
                    commandparameter.Value = c.Value;
                }
            }

            try
            {
                var executesqlcommand = await _context.Database.ExecuteSqlCommandAsync($"{commandtext} {string.Join(", ", parameters.Select(x => x.Name))}", parameters.Select(x => x.Value).ToArray()).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Exception,
                        Name = $"{this.GetType().Name}.{nameof(DeleteAsync)}",
                        Description = ex.Message
                    })
                { Exception = ex }, null);
            }

            return (new Result(), data);
        }
    }
}
