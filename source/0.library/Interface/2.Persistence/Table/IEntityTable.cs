using Library.Interface.Entities;

namespace Library.Interface.Persistence.Table
{
    public interface IEntityTable<T>
        where T : IEntity
    {
        T Entity { get; set; }
    }
}
