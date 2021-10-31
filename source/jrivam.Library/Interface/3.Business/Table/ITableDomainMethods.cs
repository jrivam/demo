using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Data;

namespace jrivam.Library.Interface.Business.Table
{
    public interface ITableDomainMethods<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        (Result result, V domain) Load(bool usedbcommand = false, int? commandtimeout = null, IDbConnection connection = null);
        (Result result, V domain) LoadQuery(int? commandtimeout = null, int maxdepth = 1, IDbConnection connection = null);

        (Result result, V domain) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false, int? commandtimeout = null, IDbConnection connection = null, IDbTransaction transaction = null);
        (Result result, V domain) Erase(bool usedbcommand = false, int? commandtimeout = null, IDbConnection connection = null, IDbTransaction transaction = null);
    }
}
