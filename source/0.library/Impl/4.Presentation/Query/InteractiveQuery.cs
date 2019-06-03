using Library.Impl.Presentation;
using Library.Impl.Domain;
using Library.Interface.Business.Query;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Table;
using Library.Interface.Presentation;
using Library.Interface.Presentation.Query;
using Library.Interface.Presentation.Raiser;
using Library.Interface.Presentation.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Impl.Presentation.Query
{
    public class InteractiveQuery<Q, R, S, T, U, V, W> : InteractiveRaiser<T, U, V, W>, IInteractiveQuery<R, S, T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : class, ITableModel<T, U, V, W>
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
            query.Status = "Loading...";

            var retrieve = query.Domain.Retrieve(maxdepth, (model != null ? model.Domain : default(V)));

            if (retrieve.result.Success && retrieve.domain != null)
            {
                var instance = Presentation.HelperInteractive<T, U, V, W>.CreateInstance(retrieve.domain, maxdepth);

                Raise(instance, maxdepth);

                instance.Status = string.Empty;

                query.Status = string.Empty;

                return (retrieve.result, model);
            }

            query.Status = String.Join("/", retrieve.result.Messages.Where(x => x.category == ResultCategory.Error || x.category == ResultCategory.Exception).ToArray()).Replace(Environment.NewLine, string.Empty);

            return (retrieve.result, default(W));
        }
        public virtual (Result result, IEnumerable<W> models) List(IQueryModel<R, S, T, U, V, W> query, int maxdepth = 1, int top = 0, IListModel<T, U, V, W> models = null)
        {
            query.Status = "Loading...";

            var enumeration = new List<W>();

            var list = query.Domain.List(maxdepth, top, (models?.Domains != null ? models?.Domains : new ListDomain<T, U, V>()));
            if (list.result.Success && list.domains != null)
            {
                foreach (var instance in RaiseDomains(list.domains, maxdepth))
                {
                    instance.Status = string.Empty;

                    enumeration.Add(instance);
                }

                query.Status = string.Empty;

                return (list.result, enumeration);
            }

            query.Status = String.Join("/", list.result.Messages.Where(x => x.category == ResultCategory.Error || x.category == ResultCategory.Exception).ToArray()).Replace(Environment.NewLine, string.Empty);

            return (list.result, default(IList<W>));
        }
    }
}
