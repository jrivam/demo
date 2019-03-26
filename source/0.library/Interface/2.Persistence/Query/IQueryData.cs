using Library.Interface.Data.Table;
using Library.Interface.Entities;

namespace Library.Interface.Data.Query
{
    public interface IQueryData<T, U> : IBuilderQueryData, IQueryDataMethods<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        void Clear();
    }
}
