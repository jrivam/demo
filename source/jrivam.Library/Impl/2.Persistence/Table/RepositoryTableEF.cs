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

namespace jrivam.Library.Impl.Persistence.Table
{
    public class RepositoryTableEF<T, U> : IRepositoryTable<T, U>
        where T : class, IEntity
        where U : class, ITableData<T, U>
    {
        protected readonly DbContext _context;

        protected readonly IDataMapper _datamapper;

        public RepositoryTableEF(DbContext context, IDataMapper datamapper)
        {
            _context = context;

            _datamapper = datamapper;
        }

        public (Result result, U data) Select(U data)
        {
            try
            {
                var find = _context.Set<T>().Find(data.Columns.Where(c => c.IsPrimaryKey).Select(x => x.Value).ToArray());

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
                        Name = nameof(Select),
                        Description = ex.Message
                    })
                { Exception = ex }, null);
            }
        }
        public (Result result, U data) Select(U data, ISqlCommand dbcommand)
        {
            foreach (var c in data.Columns.Where(c => c.IsPrimaryKey))
            {
                var parameter = Helper.GetParameter($"{c.Table.Description.Name}_{c.Description.Name}", c.Type, c.Value, ParameterDirection.Input);
                if (dbcommand.Parameters.FirstOrDefault(x => x.Name == parameter.Name) == null)
                {
                    dbcommand.Parameters.Add(parameter);
                }

            }

            return Select(data, dbcommand.Text, dbcommand.Type, dbcommand.Parameters?.ToArray());
        }
        public (Result result, U data) Select(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, ISqlParameter[] parameters = null)
        {
            try
            {
                var sqlquery = _context.Database.SqlQuery<T>($"{commandtext} {string.Join(", ", parameters.Select(x => x.Name))}", parameters.Select(x => x.Value).ToArray()).ToList();
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
                        Name = nameof(Select),
                        Description = ex.Message
                    })
                { Exception = ex }, null);
            }

            return (new Result(), data);
        }

        public (Result result, U data) Insert(U data)
        {
            try
            {
                _context.Set<T>().Add(data.Entity);
                _context.SaveChanges();

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
            //catch (Exception ex)
            //{
            //    return (new Result(
            //        new ResultMessage()
            //        {
            //            Category = ResultCategory.Exception,
            //            Name = nameof(Insert),
            //            Description = ex.Message
            //        })
            //    { Exception = ex }, null);
            //}
        }
        public (Result result, U data) Insert(U data, ISqlCommand dbcommand)
        {
            foreach (var c in data.Columns.Where(c => !c.IsIdentity))
            {
                var parameter = Helper.GetParameter($"{c.Table.Description.Name}_{c.Description.Name}", c.Type, c.Value, ParameterDirection.Input);

                var commandparameter = dbcommand.Parameters.FirstOrDefault(x => x.Name == parameter.Name);
                if (commandparameter == null)
                {
                    dbcommand.Parameters.Add(parameter);
                }
                else
                {
                    commandparameter.Value = c.Value;
                }
            }

            return Insert(data, dbcommand.Text, dbcommand.Type, dbcommand.Parameters?.ToArray());
        }
        public (Result result, U data) Insert(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, ISqlParameter[] parameters = null)
        {
            try
            {
                var executesqlcommand = _context.Database.ExecuteSqlCommand($"{commandtext} {string.Join(", ", parameters.Select(x => x.Name))}", parameters.Select(x => x.Value).ToArray());
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
                        Name = nameof(Insert),
                        Description = ex.Message
                    })
                { Exception = ex }, null);
            }

            return (new Result(), data);
        }

        public (Result result, U data) Update(U data)
        {
            try
            {
                _context.SaveChanges();

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
            //catch (Exception ex)
            //{
            //    return (new Result(
            //        new ResultMessage()
            //        {
            //            Category = ResultCategory.Exception,
            //            Name = nameof(Update),
            //            Description = ex.Message
            //        })
            //    { Exception = ex }, null);
            //}
        }
        public (Result result, U data) Update(U data, ISqlCommand dbcommand)
        {
            foreach (var c in data.Columns)
            {
                var parameter = Helper.GetParameter($"{c.Table.Description.Name}_{c.Description.Name}", c.Type, c.Value, ParameterDirection.Input);

                var commandparameter = dbcommand.Parameters.FirstOrDefault(x => x.Name == parameter.Name);
                if (commandparameter == null)
                {
                    dbcommand.Parameters.Add(parameter);
                }
                else
                {
                    commandparameter.Value = c.Value;
                }
            }

            return Update(data, dbcommand.Text, dbcommand.Type, dbcommand.Parameters?.ToArray());
        }
        public (Result result, U data) Update(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, ISqlParameter[] parameters = null)
        {
            try
            {
                var executesqlcommand = _context.Database.ExecuteSqlCommand($"{commandtext} {string.Join(", ", parameters.Select(x => x.Name))}", parameters.Select(x => x.Value).ToArray());
            }
            catch (Exception ex)
            {
                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Exception,
                        Name = nameof(Update),
                        Description = ex.Message
                    })
                { Exception = ex }, null);
            }

            return (new Result(), data);
        }

        public (Result result, U data) Delete(U data)
        {
            try
            {
                _context.Set<T>().Remove(data.Entity);
                _context.SaveChanges();

                return (new Result(), data);
            }
            catch (Exception ex)
            {
                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Exception,
                        Name = nameof(Delete),
                        Description = ex.Message
                    })
                { Exception = ex }, null);
            }
        }
        public (Result result, U data) Delete(U data, ISqlCommand dbcommand)
        {
            foreach (var c in data.Columns.Where(c => c.IsPrimaryKey))
            {
                var parameter = Helper.GetParameter($"{c.Table.Description.Name}_{c.Description.Name}", c.Type, c.Value, ParameterDirection.Input);

                var commandparameter = dbcommand.Parameters.FirstOrDefault(x => x.Name == parameter.Name);
                if (commandparameter == null)
                {
                    dbcommand.Parameters.Add(parameter);
                }
                else
                {
                    commandparameter.Value = c.Value;
                }
            }

            return Delete(data, dbcommand.Text, dbcommand.Type, dbcommand.Parameters?.ToArray());
        }
        public (Result result, U data) Delete(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, ISqlParameter[] parameters = null)
        {
            try
            {
                var executesqlcommand = _context.Database.ExecuteSqlCommand($"{commandtext} {string.Join(", ", parameters.Select(x => x.Name))}", parameters.Select(x => x.Value).ToArray());
            }
            catch (Exception ex)
            {
                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Exception,
                        Name = nameof(Delete),
                        Description = ex.Message
                    })
                { Exception = ex }, null);
            }

            return (new Result(), data);
        }
    }
}
