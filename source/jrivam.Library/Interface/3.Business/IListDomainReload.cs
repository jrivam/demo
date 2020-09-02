using jrivam.Library.Impl;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Data;

namespace jrivam.Library.Interface.Business
{
    public interface IListDomainReload<T, U, V> : IListDomainEdit<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        (Result result, IListDomain<T, U, V> domains) Refresh(int? commandtimeout = null, int top = 0, IDbConnection connection = null);
    }
}
