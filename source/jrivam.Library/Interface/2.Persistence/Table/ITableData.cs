using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Mapper;

namespace jrivam.Library.Interface.Persistence.Table
{
    public interface ITableData<T, U> : IBuilderTableData, IEntityTable<T>, ITableDataMethods<T, U>, IMapper<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
    }
}
