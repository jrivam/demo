using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Interface.Persistence.Query
{
    public interface IQueryData<T, U> : IBuilderQueryData, IQueryDataMethodsAsync<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        bool Exclude { get; set; }

        void ClearConditions();
    }
}
