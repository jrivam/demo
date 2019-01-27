using library.Interface.Data;
using library.Interface.Data.Query;
using library.Interface.Domain.Query;
using library.Interface.Presentation;
using library.Interface.Presentation.Query;

namespace library.Impl.Presentation.Query
{
    public abstract class AbstractQueryInteractive<S, R> : IQueryInteractive<S, R>, IStatus
        where S : IQueryRepository
        where R : IQueryLogic<S>, new()
    {
        public string Status { get; protected set; } = string.Empty;

        public virtual R Domain { get; protected set; }

        public virtual IQueryColumn this[string reference]
        {
            get
            {
                return Domain[reference];
            }
        }

        public AbstractQueryInteractive()
        {
        }
    }
}
