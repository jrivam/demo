using Library.Interface.Entities;

namespace Library.Interface.Data.Table
{
    public interface IEntityTable<T>
        where T : IEntity
    {
        T Entity { get; set; }
    }
}
