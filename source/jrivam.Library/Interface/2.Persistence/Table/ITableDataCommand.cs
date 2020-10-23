using jrivam.Library.Interface.Entities;

namespace jrivam.Library.Interface.Persistence.Table
{
    public interface ITableDataAuto<T, U> : ITableData<T, U>, ITableDataMethodsAuto<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
    }
}
