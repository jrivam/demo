using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;

namespace library.Impl.Domain.Table
{
    public abstract class AbstractTableLogic<T, U> : ITableLogic<T, U>
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>, ITableRepositoryMethods<T, U>, new()
    {
        public virtual U Data { get; protected set; }

        public virtual ITableColumn this[string reference]
        {
            get
            {
                return Data[reference];
            }
        }

        public virtual bool Changed { get; set; }
        public virtual bool Deleted { get; set; }

        public AbstractTableLogic()
        {
        }
    }
}
