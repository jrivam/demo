using library.Interface.Data.Table;
using library.Interface.Domain.Query;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation.Mapper;
using library.Interface.Presentation.Query;
using library.Interface.Presentation.Table;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace library.Impl.Presentation.Query
{
    public class InteractiveQuery<T, U, V, W> : Interactive<T, U, V, W>, IInteractiveQuery<T, U, V, W> 
        where T : IEntity
        where U : ITableRepositoryProperties<T>
        where V : IEntityLogicProperties<T, U>
        where W : IEntityInteractiveProperties<T, U, V>
    {
        public InteractiveQuery(IMapperInteractive<T, U, V, W> mapper)
            : base(mapper)
        {
        }

        public virtual (Result result, W presentation) Retrieve(IQueryLogicMethods<T, U, V> querylogic, int maxdepth = 1, W presentation = default(W))
        {
            var retrieve = querylogic.Retrieve(maxdepth);

            if (retrieve.result.Success && retrieve.domain != null)
            {
                presentation = (W)Activator.CreateInstance(typeof(W),
                        BindingFlags.CreateInstance |
                        BindingFlags.Public |
                        BindingFlags.Instance |
                        BindingFlags.OptionalParamBinding, 
                        null, new object[] { retrieve.domain, maxdepth }, CultureInfo.CurrentCulture);

                _mapper.Clear(presentation, maxdepth, 0);
                _mapper.Raise(presentation);
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
                    var presentation = iterator.MoveNext() ? iterator.Current : (W)Activator.CreateInstance(typeof(W),
                        BindingFlags.CreateInstance |
                        BindingFlags.Public |
                        BindingFlags.Instance |
                        BindingFlags.OptionalParamBinding, 
                        null, new object[] { domain, maxdepth }, CultureInfo.CurrentCulture);

                    _mapper.Clear(presentation, maxdepth, 0);
                    _mapper.Raise(presentation);

                    enumeration.Add(presentation);
                }
            }

            return (list.result, enumeration);
        }
    }
}
