using jrivam.Library.Impl.Persistence.Sql.Providers.MySql;
using jrivam.Library.Impl.Persistence.Sql.Providers.PostgreSql;
using jrivam.Library.Impl.Persistence.Sql.Providers.SqlServer;
using jrivam.Library.Interface.Persistence.Sql.Builder;
using System;

namespace jrivam.Library.Impl.Persistence.Sql.Factory
{
    public static class SqlSyntaxSignFactory
    {
        public static ISqlSyntaxSign Create(string providername) 
        {
            switch (providername)
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
