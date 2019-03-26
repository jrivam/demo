﻿using Library.Interface.Data.Query;
using Library.Interface.Data.Table;
using Library.Interface.Domain.Query;
using Library.Interface.Domain.Table;
using Library.Interface.Entities;
using Library.Interface.Presentation.Query;
using Library.Interface.Presentation.Raiser;
using Library.Interface.Presentation.Table;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Library.Impl.Presentation.Query
{
    public class InteractiveQuery<R, S, T, U, V, W> : Interactive<T, U, V, W>, IInteractiveQuery<R, S, T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
        where S : IQueryData<T, U>
        where R : IQueryDomain<S, T, U, V>
    {
        public InteractiveQuery(IRaiserInteractive<T, U, V, W> raiser)
            : base(raiser)
        {
        }

        public virtual (Result result, W presentation) Retrieve(IQueryModel<R, S, T, U, V, W> query, int maxdepth = 1, W presentation = default(W))
        {
            query.Status = "Loading...";

            var retrieve = query.Domain.Retrieve(maxdepth, presentation.Domain);

            if (retrieve.result.Success && retrieve.domain != null)
            {
                var instance = _raiser.CreateInstance(retrieve.domain, maxdepth);

                _raiser.Clear(instance, maxdepth, 0);
                _raiser.Raise(instance, maxdepth, 0);

                _raiser.Extra(instance, maxdepth, 0);

                query.Status = string.Empty;

                return (retrieve.result, presentation);
            }

            query.Status = String.Join("/", retrieve.result.Messages.Where(x => x.category == ResultCategory.Error).ToArray()).Replace(Environment.NewLine, string.Empty);

            return (retrieve.result, default(W));
        }
        public virtual (Result result, IEnumerable<W> presentations) List(IQueryModel<R, S, T, U, V, W> query, int maxdepth = 1, int top = 0, IList<W> presentations = null)
        {
            query.Status = "Loading...";

            var enumeration = new List<W>();
            var iterator = (presentations ?? new List<W>()).GetEnumerator();

            var list = query.Domain.List(maxdepth, top);
            if (list.result.Success && list.domains != null)
            {
                foreach (var domain in list.domains)
                {
                    var instance = iterator.MoveNext() ? iterator.Current : _raiser.CreateInstance(domain, maxdepth);

                    _raiser.Clear(instance, maxdepth, 0);
                    _raiser.Raise(instance, maxdepth, 0);

                    //_raiser.Extra(instance, maxdepth, 0);

                    enumeration.Add(instance);
                }

                query.Status = string.Empty;

                return (list.result, enumeration);
            }

            query.Status = String.Join("/", list.result.Messages.Where(x => x.category == ResultCategory.Error).ToArray()).Replace(Environment.NewLine, string.Empty);

            return (list.result, default(IList<W>));
        }
    }
}
