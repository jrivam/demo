using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Table;

namespace Library.Interface.Business.Query
{
    public interface IQueryDomain<S, T, U, V> : IBuilderQueryDomain, IDataQuery<S, T, U>, IQueryDomainMethods<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where S : IQueryData<T, U>
    {
    }
}
