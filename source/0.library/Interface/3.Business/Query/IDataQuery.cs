using Library.Interface.Entities;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Table;

namespace Library.Interface.Business.Query
{
    public interface IDataQuery<S, T, U>
        where T : IEntity
        where U : ITableData<T, U>
        where S : IQueryData<T, U>
    {
        S Data { get; }
    }
}
