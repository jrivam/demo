using library.Interface.Data.Query;
using library.Interface.Domain.Query;

namespace library.Impl.Domain.Query
{
    public abstract class AbstractQueryState<S> : IQueryState<S>
        where S : IQueryTable, new()
    {
        public virtual S Data { get; protected set; } = new S();

        public AbstractQueryState()
        {
        }
    }
}
