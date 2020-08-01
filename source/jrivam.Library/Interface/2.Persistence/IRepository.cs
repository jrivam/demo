using jrivam.Library.Impl;
using jrivam.Library.Interface.Persistence.Sql;
using System;
using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Interface.Persistence
{
    public interface IRepository
    {
        (Result result, IEnumerable<T> entities) ExecuteQuery<T>(ISqlCommand sqlcommand);
        (Result result, IEnumerable<T> entities) ExecuteQuery<T>(string commandtext, CommandType commandtype = CommandType.Text, ISqlParameter[] parameters = null);

        (Result result, int rows) ExecuteNonQuery(ISqlCommand sqlcommand);
        (Result result, int rows) ExecuteNonQuery(string commandtext, CommandType commandtype = CommandType.Text, ISqlParameter[] parameters = null);

        (Result result, T scalar) ExecuteScalar<T>(ISqlCommand sqlcommand);
        (Result result, T scalar) ExecuteScalar<T>(string commandtext, CommandType commandtype = CommandType.Text, ISqlParameter[] parameters = null);
    }
}
