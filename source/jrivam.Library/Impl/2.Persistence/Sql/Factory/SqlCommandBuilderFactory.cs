using jrivam.Library.Impl.Persistence.Sql.Providers.MySql;
using jrivam.Library.Impl.Persistence.Sql.Providers.PostgreSql;
using jrivam.Library.Impl.Persistence.Sql.Providers.SqlServer;
using jrivam.Library.Interface.Persistence.Sql.Providers;
using System;

namespace jrivam.Library.Impl.Persistence.Sql.Factory
{
    public static class SqlCommandBuilderFactory
    {
        public static ISqlCommandBuilder Create(string providername) 
        {
            switch (providername)
            {
                case "System.Data.SqlClient":
                    return new SqlServerCommandBuilder();
                case "MySql.Data.MySqlClient":
                    return new MySqlCommandBuilder();
                case "Npgsql":
                    return new PostgreSqlCommandBuilder();
                default:
                    throw new Exception();
            }
        }
    }
}
