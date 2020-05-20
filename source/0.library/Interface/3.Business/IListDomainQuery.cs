using Library.Interface.Business.Query;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Table;

namespace Library.Interface.Business
{
    public interface IListDomainQuery<R, S, T, U, V> : IListDomain<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : class, ITableDomain<T, U, V>
        where S : IQueryData<T, U>
        where R : IQueryDomain<S, T, U, V>
    {
        R Query { get; set; }
    }
}
