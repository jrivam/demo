using Library.Interface.Data.Query;
using Library.Interface.Data.Table;
using Library.Interface.Domain.Query;
using Library.Interface.Domain.Table;
using Library.Interface.Entities;

namespace Library.Interface.Presentation.Query
{
    public interface IDomainQuery<R, S, T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where S : IQueryData<T, U>
        where R : IQueryDomain<S, T, U, V>
    {
        R Domain { get; }
    }
}
