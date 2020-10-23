using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Query;

namespace jrivam.Library.Interface.Persistence.Table
{
    public interface ITableData<T, U> : IBuilderTableData
        where T : IEntity
        where U : ITableData<T, U>
    {
        T Entity { get; set; }
    }
}
