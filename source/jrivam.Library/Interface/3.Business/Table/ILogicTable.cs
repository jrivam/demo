using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Data;

namespace jrivam.Library.Interface.Business.Table
{
    public interface ILogicTable<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        (Result result, V domain) Load(V domain, bool usedbcommand = false, int? commandtimeout = null, IDbConnection connection = null);
        (Result result, V domain) LoadQuery(V domain, int? commandtimeout = null, int maxdepth = 1, IDbConnection connection = null);

        (Result result, V domain) Save(V domain, bool useinsertdbcommand = false, bool useupdatedbcommand = false, int? commandtimeout = null, IDbConnection connection = null, IDbTransaction transaction = null);
        (Result result, V domain) Erase(V domain, bool usedbcommand = false, int? commandtimeout = null, IDbConnection connection = null, IDbTransaction transaction = null);
    }
}
