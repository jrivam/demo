using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Domain.Query;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation.Query;
using library.Interface.Presentation.Table;
using System.Collections.Generic;

namespace library.Impl.Presentation.Query
{
    public abstract class AbstractQueryInteractiveMethods<S, R, T, U, V, W> : AbstractQueryInteractiveProperties<S, R>, IQueryInteractiveMethods<T, U, V, W>
        where S : IQueryRepositoryProperties
        where R : IQueryLogicProperties<S>, IQueryLogicMethods<T, U, V>, new()
        where T : IEntity
        where U : ITableRepositoryProperties<T>
        where V : ITableLogicProperties<T, U>, ITableLogicMethods<T, U, V>
        where W : ITableInteractiveProperties<T, U, V>
    {
        protected readonly IInteractiveQuery<T, U, V, W> _interactive;

        public AbstractQueryInteractiveMethods(IInteractiveQuery<T, U, V, W> interactive)
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
