using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Business
{
    public partial class ListDomainEditAsync<T, U, V> : ListDomainEdit<T, U, V>, IListDomainEditAsync<T, U, V>
        where T : IEntity
        where U : ITableDataAsync<T, U>
        where V : class, ITableDomainAsync<T, U, V>
    { 

    }
}
