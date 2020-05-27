using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Interface.Business
{
    public interface IListDomainQuery<R, S, T, U, V> : IListDomain<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where S : IQueryData<T, U>
        where R : IQueryDomain<S, T, U, V>
    {
        R Query { get; set; }
    }
}
