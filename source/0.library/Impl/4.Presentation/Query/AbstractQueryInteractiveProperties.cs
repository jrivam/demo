using library.Interface.Data.Query;
using library.Interface.Domain.Query;
using library.Interface.Presentation.Query;

namespace library.Impl.Presentation.Query
{
    public abstract class AbstractQueryInteractiveProperties<S, R> : IQueryInteractiveProperties<S, R>
        where S : IQueryRepositoryProperties
        where R : IQueryLogicProperties<S>, new()
    {
        public virtual R Domain { get; protected set; }

        public AbstractQueryInteractiveProperties()
        {
        }
    }
}
