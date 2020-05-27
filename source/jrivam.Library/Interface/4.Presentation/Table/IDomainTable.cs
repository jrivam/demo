using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;

namespace jrivam.Library.Interface.Presentation.Table
{
    public interface IDomainTable<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        V Domain { get; set; }
    }
}
