using Library.Interface.Data.Query;
using Library.Interface.Data.Table;
using Library.Interface.Entities;

namespace Library.Interface.Domain.Query
{
    public interface IDataQuery<S, T, U>
        where T : IEntity
        where U : ITableData<T, U>
        where S : IQueryData<T, U>
    {
        S Data { get; }
    }
}
