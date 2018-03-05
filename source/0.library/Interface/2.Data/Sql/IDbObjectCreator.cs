using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace library.Interface.Data.Sql
{
    public interface IDbObjectCreator
    {
        IDictionary<Type, DbType> TypeToDbType { get; }
        DbProviderFactory ProviderFactory { get; }
        IDbConnection Connection { get; }
        IDbCommand Command { get; }
        DbParameter Parameter { get; }
        DbDataAdapter Adapter { get; }
    }
}
