using library.Impl.Data.Sql.Providers.MySql;
using library.Impl.Data.Sql.Providers.PostgreSql;
using library.Impl.Data.Sql.Providers.SqlServer;
using library.Interface.Data.Sql;
using System;
using System.Configuration;

namespace library.Impl.Data.Sql.Factory
{
    public static class SqlSyntaxSignFactory
    {
        public static ISqlSyntaxSign Create(ConnectionStringSettings connectionstringsettings) 
        {
            switch (connectionstringsettings?.ProviderName)
            {
                case "System.Data.SqlClient":
                    return new SqlServerSyntaxSign();
                case "MySql.Data.MySqlClient":
                    return new MySqlSyntaxSign();
                case "Npgsql":
                    return new PostgreSqlSyntaxSign();
                default:
                    throw new Exception();
            }
        }
    }
}
