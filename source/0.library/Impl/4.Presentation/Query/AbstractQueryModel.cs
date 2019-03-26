using Library.Interface.Data.Query;
using Library.Interface.Data.Table;
using Library.Interface.Domain.Query;
using Library.Interface.Domain.Table;
using Library.Interface.Entities;
using Library.Interface.Presentation.Query;
using Library.Interface.Presentation.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Impl.Presentation.Query
{
    public abstract class AbstractQueryModel<R, S, T, U, V, W> : IQueryModel<R, S, T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
        where S : IQueryData<T, U>
        where R : IQueryDomain<S, T, U, V>, new()
    {
        public string Status { get; protected set; } = string.Empty;

        public virtual R Domain { get; protected set; }

        public virtual IColumnQuery this[string reference]
        {
            get
            {
                return Domain[reference];
            }
        }

        protected readonly IInteractiveQuery<R, S, T, U, V, W> _interactive;

        public AbstractQueryModel(R domain,
            IInteractiveQuery<R, S, T, U, V, W> interactive)
        {
            Domain = domain;

            _interactive = interactive;
        }

        public virtual (Result result, W presentation) Retrieve(int maxdepth = 1, W presentation = default(W))
        {
            Status = "Loading";
            var retrieve = _interactive.Retrieve(this, maxdepth, presentation);
            Status = String.Join("/", retrieve.result.Messages.Where(x => x.category == ResultCategory.Error).ToArray()).Replace(Environment.NewLine, string.Empty);

            return retrieve;
        }
        public virtual (Result result, IEnumerable<W> presentations) List(int maxdepth = 1, int top = 0, IList<W> presentations = null)
        {
            Status = "Loading";
            var list = _interactive.List(this, maxdepth, top, presentations);
            Status = String.Join("/", list.result.Messages.Where(x => x.category == ResultCategory.Error).ToArray()).Replace(Environment.NewLine, string.Empty);

            return list;
        }
    }
}
