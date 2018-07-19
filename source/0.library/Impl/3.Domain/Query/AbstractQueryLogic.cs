using library.Interface.Data.Query;
using library.Interface.Domain.Query;

namespace library.Impl.Domain.Query
{
    public abstract class AbstractQueryLogic<S> : IQueryLogic<S>
        where S : IQueryRepository, new()
    {
        public virtual S Data { get; protected set; }

        public virtual IQueryColumn this[string reference]
        {
            get
            {
                return Data[reference];
            }
        }

        public AbstractQueryLogic()
        {
        }
    }
}
