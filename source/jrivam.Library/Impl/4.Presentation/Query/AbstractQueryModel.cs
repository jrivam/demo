using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation.Query;
using jrivam.Library.Interface.Presentation.Table;
using System.Collections.Generic;

namespace jrivam.Library.Impl.Presentation.Query
{
    public abstract class AbstractQueryModel<T, U, V, W> : IQueryModel<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        public IQueryDomain<T, U, V> Domain { get; set; }

        public string Status { get; set; } = string.Empty;

        protected readonly IInteractiveQuery<T, U, V, W> _interactivequery;

        public AbstractQueryModel(IInteractiveQuery<T, U, V, W> interactivequery,
            IQueryDomain<T, U, V> domain)
        {
            _interactivequery = interactivequery;

            Domain = domain;
        }

        public virtual (Result result, W model) Retrieve(int maxdepth = 1)
        {
            var retrieve = _interactivequery.Retrieve(this, maxdepth);

            return retrieve;
        }
        public virtual (Result result, IEnumerable<W> models) List(int maxdepth = 1, int top = 0)
        {
            var list = _interactivequery.List(this, maxdepth, top);

            return list;
        }
    }
}
