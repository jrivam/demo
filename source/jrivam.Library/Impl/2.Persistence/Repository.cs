using jrivam.Library.Interface.Entities.Reader;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Database;
using jrivam.Library.Interface.Persistence.Sql;
using jrivam.Library.Interface.Persistence.Sql.Database;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace jrivam.Library.Impl.Persistence
{
    public class Repository : IRepository
    {
        protected readonly ConnectionStringSettings _connectionstringsettings;

        protected readonly ISqlCreator _sqlcreator;

        protected readonly IDbCommandExecutor _dbcommandexecutor;
        protected readonly IEntityReader _entityreader;

        public Repository(
            ConnectionStringSettings connectionstringsettings,
            ISqlCreator sqlcreator,
            IDbCommandExecutor dbcommandexecutor, IEntityReader entityreader)
        {
            _connectionstringsettings = connectionstringsettings;

            _sqlcreator = sqlcreator;

            _dbcommandexecutor = dbcommandexecutor;
            _entityreader = entityreader;
        }

        public virtual (Result result, IEnumerable<T> entities) ExecuteQuery<T>(
            ISqlCommand sqlcommand,
            int maxdepth = 1)
        {
            return ExecuteQuery<T>(sqlcommand.Text, sqlcommand.Type, sqlcommand.Parameters?.ToArray(), maxdepth);
        }
        public virtual (Result result, IEnumerable<T> entities) ExecuteQuery<T>(
            string commandtext, CommandType commandtype = CommandType.Text, ISqlParameter[] parameters = null,
            int maxdepth = 1)
        {
            var executequery = _dbcommandexecutor.ExecuteQuery<T>(
                _sqlcreator.GetCommand(
                        _connectionstringsettings.ProviderName,
                        _connectionstringsettings.ConnectionString,
                        commandtext, commandtype,
                        parameters),
                (x, y) => _entityreader.Read<T>(x, y, new List<string>(), maxdepth, 0));

            if (executequery.result.Success && executequery.entities?.Count() == 0)
            {
                executequery.result.SetMessage(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Information,
                        Name = nameof(ExecuteQuery),
                        Description = "No rows found."
                    }
                );
            }

            return executequery;
        }

        public virtual (Result result, int rows) ExecuteNonQuery(
            ISqlCommand sqlcommand)
        {
            return ExecuteNonQuery(sqlcommand.Text, sqlcommand.Type, sqlcommand.Parameters?.ToArray());
        }
        public virtual (Result result, int rows) ExecuteNonQuery(
            string commandtext, CommandType commandtype = CommandType.Text, ISqlParameter[] parameters = null)
        {
            var executenonquery = _dbcommandexecutor.ExecuteNonQuery(
                _sqlcreator.GetCommand(
                    _connectionstringsettings.ProviderName,
                    _connectionstringsettings.ConnectionString,
                    commandtext, commandtype, 
                    parameters));

            if (executenonquery.result.Success && executenonquery.rows == 0)
            {
                executenonquery.result.SetMessage(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Information,
                        Name = nameof(ExecuteNonQuery),
                        Description = "No rows affected."
                    }
                );
            }

            return executenonquery;
        }

        public virtual (Result result, T scalar) ExecuteScalar<T>(
            ISqlCommand sqlcommand)
        {
            return ExecuteScalar<T>(sqlcommand.Text, sqlcommand.Type, sqlcommand.Parameters?.ToArray());
        }
        public virtual (Result result, T scalar) ExecuteScalar<T>(
            string commandtext, CommandType commandtype = CommandType.Text, ISqlParameter[] parameters = null)
        {
            var executescalar = _dbcommandexecutor.ExecuteScalar<T>(
                _sqlcreator.GetCommand(
                    _connectionstringsettings.ProviderName,
                    _connectionstringsettings.ConnectionString,
                    commandtext, commandtype, 
                    parameters));

            if (executescalar.result.Success && executescalar.scalar == null)
            {
                executescalar.result.SetMessage(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Information,
                        Name = nameof(ExecuteScalar),
                        Description = "No rows affected."
                    }
                );
            }

            return executescalar;
        }
    }
}
