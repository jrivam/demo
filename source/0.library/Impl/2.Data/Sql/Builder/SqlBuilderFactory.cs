using library.Impl.Data.Sql.SyntaxSign;
using library.Interface.Data.Sql;
using library.Interface.Entities;
using System;
using System.Configuration;

namespace library.Impl.Data.Sql.Builder
{
    public static class SqlBuilderFactory<U> 
        where U : IEntity
    {
        public static ISqlBuilder<U> GetBuilder(string connectionstringname) 
        {
            var connectionstringsettings = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[connectionstringname]];

            switch (connectionstringsettings?.ProviderName)
            {
                case "MySql.Data.MySqlClient":
                    return new MySqlBuilder<U>(connectionstringsettings, new MySqlSyntaxSign());
                case "System.Data.SqlClient":
                    return new SqlServerBuilder<U>(connectionstringsettings, new SqlServerSyntaxSign());
                default:
                    throw new Exception();
            }
        }
    }
}
