using jrivam.Library.Impl.Business;
using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation;
using jrivam.Library.Interface.Presentation.Query;
using jrivam.Library.Interface.Presentation.Raiser;
using jrivam.Library.Interface.Presentation.Table;
using System.Collections.Generic;
using System.Linq;

namespace jrivam.Library.Impl.Presentation.Query
{
    public class InteractiveQuery<T, U, V, W> : IInteractiveQuery<T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : class, ITableDomain<T, U, V>
        where W : class, ITableModel<T, U, V, W>
    {
        protected readonly IInteractive<T, U, V> _interactive;

        protected readonly ILogicQuery<T, U, V> _logicquery;

        protected readonly IModelRaiser _raiser;

        public InteractiveQuery(IInteractive<T, U, V> interactive,
            ILogicQuery<T, U, V> logicquery,
            IModelRaiser raiser)
        {
            _interactive = interactive;

            _logicquery = logicquery;

            _raiser = raiser;
        }

        public virtual (Result result, W model) Retrieve(IQueryModel<T, U, V, W> query, int maxdepth = 1, W model = default(W))
        {
            query.Status = "Retrieving...";

            var list = List(query, maxdepth, 1, model != null ? new ListModel<T, U, V, W>() { model } : null);

            query.Status = list.result.FilteredAsText("/", x => x.category == (x.category & ResultCategory.OnlyErrors));

            return (list.result, list.models.FirstOrDefault());
        }
        public virtual (Result result, IEnumerable<W> models) List(IQueryModel<T, U, V, W> query, int maxdepth = 1, int top = 0, IListModel<T, U, V, W> models = null)
        {
            query.Status = "Listing...";

            var list = _logicquery.List(query.Domain, maxdepth, top, models?.Domains ?? new ListDomain<T, U, V>());
            if (list.result.Success)
            {
                var enumeration = new List<W>();

                if (list.domains != null)
                {
                    foreach (var domain in list.domains)
                    {
                        var instance = Presentation.HelperTableInteractive<T, U, V, W>.CreateModel(domain, maxdepth);

                        _raiser.Raise<T, U, V, W>(instance, maxdepth);

                        instance.Status = string.Empty;

                        enumeration.Add(instance);
                    }
                }

                query.Status = string.Empty;

                return (list.result, enumeration);
            }

            query.Status = list.result.FilteredAsText("/", x => x.category == (x.category & ResultCategory.OnlyErrors));

            return (list.result, default(IList<W>));
        }
    }
}
