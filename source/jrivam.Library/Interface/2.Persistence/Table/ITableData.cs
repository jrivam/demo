using jrivam.Library.Interface.Entities;

namespace jrivam.Library.Interface.Persistence.Table
{
    public interface ITableData<T, U> : IBuilderTableData, ITableDataMethodsAsync<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        T Entity { get; set; }
    }
}
