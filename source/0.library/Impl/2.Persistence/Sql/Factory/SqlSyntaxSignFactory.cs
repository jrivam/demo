using Library.Impl.Data.Sql.Providers.MySql;
using Library.Impl.Data.Sql.Providers.PostgreSql;
using Library.Impl.Data.Sql.Providers.SqlServer;
using Library.Interface.Data.Sql.Builder;
using System;
using System.Configuration;

namespace Library.Impl.Data.Sql.Factory
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
