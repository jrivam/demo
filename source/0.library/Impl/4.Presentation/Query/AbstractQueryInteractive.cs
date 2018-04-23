using library.Interface.Data.Model;
using library.Interface.Data.Query;
using library.Interface.Domain.Model;
using library.Interface.Domain.Query;
using library.Interface.Entities;
using library.Interface.Presentation.Model;
using library.Interface.Presentation.Query;
using System.Collections.Generic;

namespace library.Impl.Presentation.Query
{
    public abstract class AbstractQueryInteractive<S, R, T, U, V, W> : AbstractQueryView<S, R>, IQueryInteractive<T, U, V, W>
        where S : IQueryTable
        where R : IQueryState<S>, IQueryLogic<T, U, V>, new()
        where T : IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>, IEntityLogic<T, U, V>
        where W : IEntityView<T, U, V>
    {
        public IInteractiveQuery<T, U, V, W> _interactive { get; protected set; }

        public AbstractQueryInteractive(IInteractiveQuery<T, U, V, W> interactive)
            : base()
        {
            _interactive = interactive;
        }

        public virtual (Result result, W presentation) Retrieve(int maxdepth = 1, W presentation = default(W))
        {
            return _interactive.Retrieve(Domain, maxdepth, presentation);
        }
        public virtual (Result result, IEnumerable<W> presentations) List(int maxdepth = 1, int top = 0, IList<W> presentations = null)
        {
            return _interactive.List(Domain, maxdepth, top, presentations);
        }
    }
}
