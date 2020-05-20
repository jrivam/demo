using Library.Interface.Entities;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Table;

namespace Library.Interface.Persistence
{
    public interface IListDataQuery<S, T, U> : IListData<T, U>
        where T : IEntity
        where U : ITableData<T, U>
        where S : IQueryData<T, U>
    {
        S Query { get; set; }
    }
}
