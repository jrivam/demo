using Library.Interface.Entities;
using Library.Interface.Persistence.Table;

namespace Library.Interface.Persistence.Query
{
    public interface IQueryData<T, U> : IBuilderQueryData, IQueryDataMethods<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        void Clear();

        void Init();
    }
}
