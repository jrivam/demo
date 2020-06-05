using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Interface.Business.Table
{
    public interface ITableDomain<T, U, V> : IBuilderTableDomain, ITableDomainMethods<T, U, V>, ITableDomainValidation
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        U Data { get; set; }

        bool Changed { get; set; }
        bool Deleted { get; set; }
    }
}
