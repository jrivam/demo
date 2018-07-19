using library.Interface.Data.Table;
using library.Interface.Domain.Query;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation.Query;
using library.Interface.Presentation.Raiser;
using library.Interface.Presentation.Table;
using System.Collections.Generic;

namespace library.Impl.Presentation.Query
{
    public class InteractiveQuery<T, U, V, W> : Interactive<T, U, V>, IInteractiveQuery<T, U, V, W> 
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where V : ITableLogic<T, U>
        where W : ITableInteractive<T, U, V>
    {
        protected readonly IRaiserInteractive<T, U, V, W> _raiser;

        public InteractiveQuery(IRaiserInteractive<T, U, V, W> raiser)
            : base()
        {
            _raiser = raiser;
        }

        public virtual (Result result, W presentation) Retrieve(IQueryLogicMethods<T, U, V> querylogic, int maxdepth = 1, W presentation = default(W))
        {
            var retrieve = querylogic.Retrieve(maxdepth);

            if (retrieve.result.Success && retrieve.domain != null)
            {
                presentation = _raiser.CreateInstance(retrieve.domain, maxdepth);

                _raiser.Clear(presentation, maxdepth, 0);
                _raiser.Raise(presentation, maxdepth, 0);
            }

            return (retrieve.result, presentation);
        }
        public virtual (Result result, IEnumerable<W> presentations) List(IQueryLogicMethods<T, U, V> querylogic, int maxdepth = 1, int top = 0, IList<W> presentations = null)
        {
            var enumeration = new List<W>();
            var iterator = (presentations ?? new List<W>()).GetEnumerator();

            var list = querylogic.List(maxdepth, top);
            if (list.result.Success && list.domains != null)
            {
                foreach (var domain in list.domains)
                {
                    var presentation = iterator.MoveNext() ? iterator.Current : _raiser.CreateInstance(domain, maxdepth);

                    _raiser.Clear(presentation, maxdepth, 0);
                    _raiser.Raise(presentation, maxdepth, 0);

                    enumeration.Add(presentation);
                }
            }

            return (list.result, enumeration);
        }
    }
}
