using library.Interface.Data;
using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;

namespace library.Impl.Domain.Table
{
    public abstract class AbstractEntityLogicProperties<T, U> : IEntityLogicProperties<T, U>
        where T : IEntity
        where U : IEntityRepositoryProperties<T>, IEntityRepositoryMethods<T, U>, new()
    {
        public virtual U Data { get; protected set; }

        public virtual IEntityColumn this[string reference]
        {
            get
            {
                return Data[reference];
            }
        }

        public virtual bool Changed { get; set; }
        public virtual bool Deleted { get; set; }

        public AbstractEntityLogicProperties()
        {
        }
    }
}
