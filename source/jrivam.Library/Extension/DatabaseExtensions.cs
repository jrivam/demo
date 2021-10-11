using Autofac;
using jrivam.Library.Impl;
using jrivam.Library.Impl.Persistence;
using jrivam.Library.Interface.Persistence.Sql;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace jrivam.Library.Extension
{
    public static class DatabaseExtensions
    {
        public static async Task<(Result result, IEnumerable<T> entities)> ExecuteQueryAsync<T>(this IDbConnection connection,
            string commandtext, CommandType commandtype = CommandType.Text, 
            ISqlParameter[] parameters = null,
            int maxdepth = 1,
            int commandtimeout = 30)
        {
            return await AutofacConfiguration.Container.Resolve<RepositoryAsync>().ExecuteQueryAsync<T>(commandtext, commandtype, parameters, maxdepth, connection, commandtimeout).ConfigureAwait(false);
        }

        public static DbConnection DbConnection(this IDbCommand command)
        {
            return ((DbConnection)command.Connection);
        }
    }
}
