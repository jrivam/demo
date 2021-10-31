using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Interface.Business.Query
{
    public interface IDataQuery<S, T, U>
        where T : IEntity
        where U : ITableData<T, U>
        where S : IQueryData<T, U>
    {
        S Data { get; }
    }
}
