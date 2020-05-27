using System.Data;
using System.Data.Common;

namespace jrivam.Library.Interface.Persistence.Database
{
    public interface IDbObjectCreator
    {
        DbProviderFactory ProviderFactory { get; }
        IDbConnection Connection { get; }
        IDbCommand Command { get; }
        DbParameter Parameter { get; }
        DbDataAdapter Adapter { get; }
    }
}
