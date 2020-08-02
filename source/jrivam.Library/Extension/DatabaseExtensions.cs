using Autofac;
using jrivam.Library.Impl;
using jrivam.Library.Impl.Persistence;
using jrivam.Library.Interface.Persistence.Sql;
using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Extension
{
    public static class DatabaseExtensions
    {
        public static (Result result, IEnumerable<T> entities) ExecuteQuery<T>(this IDbConnection connection,
            string commandtext, CommandType commandtype = CommandType.Text, ISqlParameter[] parameters = null,
            int maxdepth = 1)
        {
            return AutofacConfiguration.Container.Resolve<Repository>().ExecuteQuery<T>(commandtext, commandtype, parameters, maxdepth, connection);
        }
    }
}
