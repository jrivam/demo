using Library.Impl;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;

namespace Library.Interface.Business
{
    public interface IListDomainRefresh<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        (Result result, IListDomain<T, U, V> domains) Refresh(int top = 0);
    }
}
