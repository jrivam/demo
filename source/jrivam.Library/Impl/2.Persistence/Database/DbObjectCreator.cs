using jrivam.Library.Interface.Persistence.Database;
using System.Data;
using System.Data.Common;

namespace jrivam.Library.Impl.Persistence.Database
{
    public class DbObjectCreator : IDbObjectCreator
    {
        public virtual DbProviderFactory GetProviderFactory(string providername)
        {
            return DbProviderFactories.GetFactory(providername);
        }

        public virtual IDbConnection GetConnection(string providername, string connectionstring = "")
        {
            var connection = GetProviderFactory(providername).CreateConnection();
            connection.ConnectionString = connectionstring;

            return connection;
        }

        public virtual IDbCommand GetCommand(string providername)
        {
            return GetProviderFactory(providername).CreateCommand();
        }
        public virtual DbParameter GetParameter(string providername)
        {
            return GetProviderFactory(providername).CreateParameter();
        }
        public virtual DbDataAdapter GetAdapter(string providername)
        {
            return GetProviderFactory(providername).CreateDataAdapter();
        }
    }
}
