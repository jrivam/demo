using jrivam.Library.Interface.Entities;

namespace jrivam.Library.Interface.Persistence.Table
{
    public interface IEntityTable<T>
        where T : IEntity
    {
        T Entity { get; set; }
    }
}
