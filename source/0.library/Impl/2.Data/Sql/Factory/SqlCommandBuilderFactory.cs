using library.Impl.Data.Sql.Providers.MySql;
using library.Impl.Data.Sql.Providers.PostgreSql;
using library.Impl.Data.Sql.Providers.SqlServer;
using library.Interface.Data.Sql;
using System;
using System.Configuration;

namespace library.Impl.Data.Sql.Factory
{
    public static class SqlCommandBuilderFactory
    {
        public static ISqlCommandBuilder Create(ConnectionStringSettings connectionstringsettings) 
        {
            switch (connectionstringsettings?.ProviderName)
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
