using Library.Interface.Data.Query;
using Library.Interface.Data.Table;
using Library.Interface.Domain.Table;
using Library.Interface.Entities;

namespace Library.Interface.Domain.Query
{
    public interface IQueryDomain<S, T, U, V> : IBuilderQueryDomain, IDataQuery<S, T, U>, IQueryDomainMethods<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where S : IQueryData<T, U>
    {
    }
}
