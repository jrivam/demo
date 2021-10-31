using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Interface.Persistence
{
    public interface IListDataQuery<S, T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        S Query { get; set; }
    }
}
