using library.Interface.Entities;

namespace library.Interface.Data.Table
{
    public interface ITableEntity<T>
        where T : IEntity
    {
        T Entity { get; }
    }
}
