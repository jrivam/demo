using library.Impl.Data.Sql.Providers.MySql;
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
                case "MySql.Data.MySqlClient":
                    return new MySqlBuilderQuery(syntaxsign);
                case "System.Data.SqlClient":
                    return new SqlServerBuilderQuery(syntaxsign);
                default:
                    throw new Exception();
            }
        }
    }
}
