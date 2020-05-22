using Library.Interface.Business.Query;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Table;
using Library.Interface.Presentation;
using Library.Interface.Presentation.Query;
using Library.Interface.Presentation.Table;
using System.Collections.Generic;

namespace Library.Impl.Presentation.Query
{
    public abstract class AbstractQueryModel<Q, R, S, T, U, V, W> : IQueryModel<R, S, T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
        where S : IQueryData<T, U>
        where R : IQueryDomain<S, T, U, V>
        where Q : IQueryModel<R, S, T, U, V, W>
    {
        public string Status { get; set; } = string.Empty;

        public virtual R Domain { get; protected set; }

        public virtual IColumnQuery this[string name]
        {
            get
            {
                return Domain[name];
            }
        }

        protected readonly IInteractiveQuery<R, S, T, U, V, W> _interactive;

        public AbstractQueryModel(R domain,
            IInteractiveQuery<R, S, T, U, V, W> interactive)
        {
            Domain = domain;

            _interactive = interactive;
        }

        public virtual (Result result, W model) Retrieve(int maxdepth = 1, W model = default(W))
        {
            var retrieve = _interactive.Retrieve(this, maxdepth, model);

            return retrieve;
        }
        public virtual (Result result, IEnumerable<W> models) List(int maxdepth = 1, int top = 0, IListModel<T, U, V, W> models = null)
        {
            var list = _interactive.List(this, maxdepth, top, models);

            return list;
        }
    }
}
