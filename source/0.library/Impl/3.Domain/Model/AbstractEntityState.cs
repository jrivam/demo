using library.Interface.Data.Model;
using library.Interface.Domain.Model;
using library.Interface.Entities;

namespace library.Impl.Domain.Model
{
    public abstract class AbstractEntityState<T, U> : IEntityState<T, U>
        where T : IEntity
        where U : IEntityTable<T>, IEntityRepository<T, U>, new()
    {
        public virtual U Data { get; protected set; } = new U();

        public virtual bool Changed { get; set; }
        public virtual bool Deleted { get; set; }
        public AbstractEntityState()
        {
        }
    }
}
