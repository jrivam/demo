using library.Interface.Data.Query;
using library.Interface.Domain.Query;
using library.Interface.Presentation.Query;

namespace library.Impl.Presentation.Query
{
    public abstract class AbstractQueryView<S, R> : IQueryView<S, R>
        where S : IQueryTable
        where R : IQueryState<S>, new()
    {
        public virtual R Domain { get; protected set; } = new R();

        public AbstractQueryView()
        {
        }
    }
}
