using Library.Interface.Data.Table;
using Library.Interface.Entities;

namespace Library.Interface.Domain.Table
{
    public interface ITableDomain<T, U, V> : IBuilderTableDomain, IDataTable<T, U>, ITableDomainMethods<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        bool Changed { get; set; }
        bool Deleted { get; set; }
    }
}
