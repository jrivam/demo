using jrivam.Library.Impl;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Interface.Business
{
    public interface IListDomainEditAsync<T, U, V> : IListDomainEdit<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        Task<Result> SaveAllAsync(IDbConnection connection = null, IDbTransaction transaction = null, int? commandtimeout = null);
        Task<Result> EraseAllAsync(IDbConnection connection = null, IDbTransaction transaction = null, int? commandtimeout = null);
    }
}
