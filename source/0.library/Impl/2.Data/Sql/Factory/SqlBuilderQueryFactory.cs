using library.Impl.Data.Sql.Providers.MySql;
using library.Impl.Data.Sql.Providers.PostgreSql;
using library.Impl.Data.Sql.Providers.SqlServer;
using library.Interface.Data.Sql;
using System;
using System.Configuration;

namespace library.Impl.Data.Sql.Factory
{
    public static class SqlBuilderQueryFactory
    {
        public static ISqlBuilderQuery Create(ConnectionStringSettings connectionstringsettings)
        {
            return Create(connectionstringsettings, SqlSyntaxSignFactory.Create(connectionstringsettings));
        }
        public static ISqlBuilderQuery Create(ConnectionStringSettings connectionstringsettings, ISqlSyntaxSign syntaxsign) 
        {
            switch (connectionstringsettings?.ProviderName)
            {
                case "System.Data.SqlClient":
                    return new SqlServerBuilderQuery(syntaxsign);
                case "MySql.Data.MySqlClient":
                    return new MySqlBuilderQuery(syntaxsign);
                case "Npgsql":
                    return new PostgreSqlBuilderQuery(syntaxsign);
                default:
                    throw new Exception();
            }
        }
    }
}
