﻿using library.Interface.Data.Model;
using library.Interface.Domain.Model;
using library.Interface.Domain.Query;
using library.Interface.Entities;
using library.Interface.Presentation.Mapper;
using library.Interface.Presentation.Model;
using library.Interface.Presentation.Query;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace library.Impl.Presentation.Query
{
    public class InteractiveQuery<T, U, V, W> : Interactive<T, U, V, W>, IInteractiveQuery<T, U, V, W> 
        where T : IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>
        where W : IEntityView<T, U, V>
    {
        public InteractiveQuery(IMapperView<T, U, V, W> mapper)
            : base(mapper)
        {
        }

        public virtual (Result result, W presentation) Retrieve(IQueryLogic<T, U, V> querylogic, int maxdepth = 1, W presentation = default(W))
        {
            if (presentation == null)
            {
                presentation = (W)Activator.CreateInstance(typeof(W),
                        BindingFlags.CreateInstance |
                        BindingFlags.Public |
                        BindingFlags.Instance |
                        BindingFlags.OptionalParamBinding, null, null, CultureInfo.CurrentCulture);
            }

            var retrieve = querylogic.Retrieve(maxdepth, presentation.Domain);

            if (retrieve.result.Success)
            {
                _mapper.Clear(presentation);
                _mapper.Map(presentation);
            }

            return (retrieve.result, presentation);
        }
        public virtual (Result result, IEnumerable<W> presentations) List(IQueryLogic<T, U, V> querylogic, int maxdepth = 1, int top = 0, IList<W> presentations = null)
        {
            var enumeration = new List<W>();
            var iterator = (presentations ?? new List<W>()).GetEnumerator();

            var list = querylogic.List(maxdepth, top);
            foreach (var domain in list.domains)
            {
                var presentation = iterator.MoveNext() ? iterator.Current : (W)Activator.CreateInstance(typeof(W),
                    BindingFlags.CreateInstance |
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.OptionalParamBinding, null, new object[] { domain, maxdepth }, CultureInfo.CurrentCulture);

                _mapper.Clear(presentation);
                _mapper.Map(presentation);

                enumeration.Add(presentation);
            }

            return (list.result, enumeration);
        }
    }
}