using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;

namespace Library.Interface.Business.Table
{
    public interface ITableDomain<T, U, V> : IBuilderTableDomain, IDataTable<T, U>, ITableDomainMethods<T, U, V>, ITableDomainValidation
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        bool Changed { get; set; }
        bool Deleted { get; set; }
    }
}
