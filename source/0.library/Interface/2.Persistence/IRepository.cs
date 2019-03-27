using Library.Interface.Entities;
using Library.Interface.Persistence.Table;

namespace library.Interface.Persistence
{
    public interface IRepository<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
    }
}
