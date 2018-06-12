using library.Impl.Data.Sql.Providers.MySql;
using library.Impl.Data.Sql.Providers.SqlServer;
using library.Interface.Data.Sql;
using library.Interface.Entities;
using System;
using System.Configuration;

namespace library.Impl.Data.Sql.Factory
{
    public static class SqlBuilderTableFactory<T> 
        where T : IEntity
    {
        public static ISqlBuilderTable<T> Create(ConnectionStringSettings connectionstringsettings)
        {
            return Create(connectionstringsettings, SqlSyntaxSignFactory.Create(connectionstringsettings));
        }
        public static ISqlBuilderTable<T> Create(ConnectionStringSettings connectionstringsettings, ISqlSyntaxSign syntaxsign) 
        {
            switch (connectionstringsettings?.ProviderName)
            {
                case "MySql.Data.MySqlClient":
                    return new MySqlBuilderTable<T>(syntaxsign);
                case "System.Data.SqlClient":
                    return new SqlServerBuilderTable<T>(syntaxsign);
                default:
                    throw new Exception();
            }
        }
    }
}
