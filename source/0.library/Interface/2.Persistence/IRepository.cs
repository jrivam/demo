using Library.Interface.Data.Table;
using Library.Interface.Entities;

namespace library.Interface.Data
{
    public interface IRepository<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
    }
}
