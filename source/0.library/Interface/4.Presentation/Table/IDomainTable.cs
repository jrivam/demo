using Library.Interface.Persistence.Table;
using Library.Interface.Business.Table;
using Library.Interface.Entities;

namespace Library.Interface.Presentation.Table
{
    public interface IDomainTable<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        //T Entity { get; set; }

        V Domain { get; set; }
    }
}
