using System.Data;
using System.Data.Common;

namespace library.Interface.Data.Sql
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
