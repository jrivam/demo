using Library.Interface.Entities;

namespace Library.Interface.Persistence.Table
{
    public interface ITableData<T, U> : IBuilderTableData, IEntityTable<T>, ITableDataMethods<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        void Init();
    }
}
