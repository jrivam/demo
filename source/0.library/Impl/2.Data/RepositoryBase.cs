using library.Impl.Data.Sql;
using library.Interface.Data;
using library.Interface.Data.Sql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace library.Impl.Data
{
    public class RepositoryBase : IRepositoryBase 
    {
        protected readonly ISqlCreator _creator;

        public RepositoryBase(ISqlCreator creator)
        {
            _creator = creator;
        }
        public RepositoryBase(ConnectionStringSettings connectionstringsettings)
            : this(new SqlCreator(connectionstringsettings))
        {
        }
        public RepositoryBase(string connectionstringname)
            : this(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[connectionstringname]])
        {
        }

        public virtual (Result result, int rows) ExecuteNonQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null)
        {
            return ExecuteNonQuery(_creator.GetCommand(commandtext, commandtype, parameters));
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
                return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Exception, $"{ex.Message}{Environment.NewLine}{ex.InnerException}") } }, -1);
            }
        }

        public virtual (Result result, object scalar) ExecuteScalar(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null)
        {
            return ExecuteScalar(_creator.GetCommand(commandtext, commandtype, parameters));
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
                return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Exception, $"{ex.Message}{Environment.NewLine}{ex.InnerException}") } }, null);
            }
        }
    }
}
