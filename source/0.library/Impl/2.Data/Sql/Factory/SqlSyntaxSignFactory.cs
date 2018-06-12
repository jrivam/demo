using library.Impl.Data.Sql.Providers.MySql;
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
                case "MySql.Data.MySqlClient":
                    return new MySqlSyntaxSign();
                case "System.Data.SqlClient":
                    return new SqlServerSyntaxSign();
                default:
                    throw new Exception();
            }
        }
    }
}
