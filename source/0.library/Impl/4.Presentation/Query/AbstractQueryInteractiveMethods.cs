using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Domain.Query;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation.Query;
using library.Interface.Presentation.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace library.Impl.Presentation.Query
{
    public abstract class AbstractQueryInteractiveMethods<S, R, T, U, V, W> : AbstractQueryInteractive<S, R>, IQueryInteractiveMethods<T, U, V, W>
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where V : ITableLogic<T, U>, ITableLogicMethods<T, U, V>
        where W : ITableInteractive<T, U, V>
        where S : IQueryRepository
        where R : IQueryLogic<S>, IQueryLogicMethods<T, U, V>, new()
    {
        protected readonly IInteractiveQuery<T, U, V, W> _interactive;

        public AbstractQueryInteractiveMethods(R domain,
            IInteractiveQuery<T, U, V, W> interactive)
            : base(domain)
        {
            _interactive = interactive;
        }

        public virtual (Result result, W presentation) Retrieve(int maxdepth = 1, W presentation = default(W))
        {
            Status = "Loading";
            var retrieve = _interactive.Retrieve(Domain, maxdepth, presentation);
            Status = String.Join("/", retrieve.result.Messages.Where(x => x.category == ResultCategory.Error).ToArray()).Replace(Environment.NewLine, string.Empty);

            return retrieve;
        }
        public virtual (Result result, IEnumerable<W> presentations) List(int maxdepth = 1, int top = 0, IList<W> presentations = null)
        {
            Status = "Loading";
            var list = _interactive.List(Domain, maxdepth, top, presentations);
            Status = String.Join("/", list.result.Messages.Where(x => x.category == ResultCategory.Error).ToArray()).Replace(Environment.NewLine, string.Empty);

            return list;
        }
    }
}
