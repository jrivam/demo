using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Interface.Business
{
    public interface IListDomainQuery<S, T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : class, ITableDomain<T, U, V>
        where S : IQueryDomain<T, U, V>
    {
        S Query { get; set; }
    }
}
