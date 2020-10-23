using jrivam.Library.Impl;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Interface.Business
{
    public interface IListDomainReloadAsync<T, U, V> : IListDomainEditAsync<T, U, V>, IListDomainReload<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        Task<(Result result, IListDomain<T, U, V> domains)> RefreshAsync(int top = 0, IDbConnection connection = null, int? commandtimeout = null);
    }
}
