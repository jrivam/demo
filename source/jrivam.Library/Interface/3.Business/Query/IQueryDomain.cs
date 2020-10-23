using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Interface.Business.Query
{
    public interface IQueryDomain<T, U, V> : IQueryDomainMethodsAsync<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        IQueryData<T, U> Data { get; set; }
    }
}
