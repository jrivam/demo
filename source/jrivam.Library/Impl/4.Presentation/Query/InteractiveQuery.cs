using jrivam.Library.Impl.Business;
using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation;
using jrivam.Library.Interface.Presentation.Query;
using jrivam.Library.Interface.Presentation.Raiser;
using jrivam.Library.Interface.Presentation.Table;
using System.Collections.Generic;

namespace jrivam.Library.Impl.Presentation.Query
{
    public class InteractiveQuery<Q, R, S, T, U, V, W> : InteractiveRaiser<T, U, V, W>, IInteractiveQuery<R, S, T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
        where S : IQueryData<T, U>
        where R : IQueryDomain<S, T, U, V>
        where Q : IQueryModel<R, S, T, U, V, W>
    {
        public InteractiveQuery(IRaiser<T, U, V, W> raiser)
            : base(raiser)
        {
        }

        public virtual (Result result, W model) Retrieve(IQueryModel<R, S, T, U, V, W> query, int maxdepth = 1, W model = default(W))
        {
            query.Status = "Retrieving...";

            var retrieve = query.Domain.Retrieve(maxdepth, model != null ? model.Domain : default(V));
            if (retrieve.result.Success)
            {
                if (retrieve.domain != null)
                {
                    var instance = Presentation.HelperTableInteractive<T, U, V, W>.CreateModel(retrieve.domain, maxdepth);

                    Raise(instance, maxdepth);

                    instance.Status = string.Empty;

                    query.Status = string.Empty;

                    return (retrieve.result, model);
                }
            }

            query.Status = retrieve.result.FilteredAsText("/", x => x.category == (x.category & ResultCategory.OnlyErrors));

            return (retrieve.result, default(W));
        }
        public virtual (Result result, IEnumerable<W> models) List(IQueryModel<R, S, T, U, V, W> query, int maxdepth = 1, int top = 0, IListModel<T, U, V, W> models = null)
        {
            query.Status = "Listing...";

            var list = query.Domain.List(maxdepth, top, models?.Domains ?? new ListDomain<T, U, V>());
            if (list.result.Success)
            {
                var enumeration = new List<W>();

                if (list.domains != null)
                {
                    foreach (var instance in RaiseDomains(list.domains, maxdepth))
                    {
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
