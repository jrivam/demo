using Dapper;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Sql;
using jrivam.Library.Interface.Persistence.Sql.Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace jrivam.Library.Impl.Persistence
{
    public class RepositoryDapper : IRepository
    {
        protected readonly ConnectionStringSettings _connectionstringsettings;

        protected readonly ISqlCreator _sqlcreator;

        public RepositoryDapper(
            ConnectionStringSettings connectionstringsettings,
            ISqlCreator sqlcreator)
        {
            _connectionstringsettings = connectionstringsettings;

            _sqlcreator = sqlcreator;
        }

        public virtual (Result result, IEnumerable<T> entities) ExecuteQuery<T>(
            ISqlCommand sqlcommand,
            int maxdepht = 1)
        {
            return ExecuteQuery<T>(sqlcommand.Text, sqlcommand.Type, sqlcommand.Parameters.ToArray(), maxdepht);
        }
        public virtual (Result result, IEnumerable<T> entities) ExecuteQuery<T>(
            string commandtext, CommandType commandtype = CommandType.Text, ISqlParameter[] parameters = null,
            int maxdepht = 1)
        {
            try
            {
                using (var connection = _sqlcreator.GetConnection(
                    _connectionstringsettings.ProviderName,
                    _connectionstringsettings.ConnectionString))
                {
                    var p = new DynamicParameters();

                    foreach(var parameter in parameters)
                    {
                        p.Add(parameter.Name, parameter.Value);
                    }

                    var r = connection.Query<T>(commandtext, p);

                    return (new Result(), r);
                }

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

        public virtual (Result result, int rows) ExecuteNonQuery(
            ISqlCommand sqlcommand)
        {
            return ExecuteNonQuery(sqlcommand.Text, sqlcommand.Type, sqlcommand.Parameters?.ToArray());
        }
        public virtual (Result result, int rows) ExecuteNonQuery(
            string commandtext, CommandType commandtype = CommandType.Text, ISqlParameter[] parameters = null)
        {
            try
            {
                using (var connection = _sqlcreator.GetConnection(
                    _connectionstringsettings.ProviderName,
                    _connectionstringsettings.ConnectionString))
                {
                    var p = new DynamicParameters();

                    foreach (var parameter in parameters)
                    {
                        p.Add(parameter.Name, parameter.Value);
                    }

                    return (new Result(), connection.Query<int>(commandtext, p).FirstOrDefault());
                }

            }
            catch (Exception ex)
            {
                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Exception,
                        Name = nameof(ExecuteNonQuery),
                        Description = ex.Message
                    })
                { Exception = ex }, -1);
            }
        }

        public virtual (Result result, T scalar) ExecuteScalar<T>(
            ISqlCommand sqlcommand)
        {
            return ExecuteScalar<T>(sqlcommand.Text, sqlcommand.Type, sqlcommand.Parameters?.ToArray());
        }
        public virtual (Result result, T scalar) ExecuteScalar<T>(
            string commandtext, CommandType commandtype = CommandType.Text, ISqlParameter[] parameters = null)
        {
            try
            {
                using (var connection = _sqlcreator.GetConnection(
                    _connectionstringsettings.ProviderName,
                    _connectionstringsettings.ConnectionString))
                {
                    var p = new DynamicParameters();

                    foreach (var parameter in parameters)
                    {
                        p.Add(parameter.Name, parameter.Value);
                    }

                    var execute = connection.Execute(commandtext, p);

                    return (new Result(), (T)Convert.ChangeType(execute, typeof(T)));
                }

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
