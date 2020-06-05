using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Mapper;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Interface.Persistence.Query
{
    public interface IQueryData<T, U> : IBuilderQueryData, IQueryDataMethods<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        void ClearConditions();

        void Init();
    }
}
