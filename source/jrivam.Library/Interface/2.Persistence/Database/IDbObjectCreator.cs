using System.Data;
using System.Data.Common;

namespace jrivam.Library.Interface.Persistence.Database
{
    public interface IDbObjectCreator
    {
        DbProviderFactory GetProviderFactory(string providername);

        IDbConnection GetConnection(string providername, string connectionstring = "");

        IDbCommand GetCommand(string providername);
        DbParameter GetParameter(string providername);
        DbDataAdapter GetAdapter(string providername);
    }
}
