using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Interface.Business
{
    public interface IListDomainReload<T, U, V> : IListDomainEdit<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
    }
}
