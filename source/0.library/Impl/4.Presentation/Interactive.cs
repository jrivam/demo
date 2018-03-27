﻿using library.Interface.Data;
using library.Interface.Domain;
using library.Interface.Entities;
using library.Interface.Presentation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace library.Impl.Business
{
    public class Interactive<T, U, V, W> : IInteractive<T, U, V, W> where T : IEntity
                                                                    where U : IEntityTable<T>
                                                                    where V : IEntityState<T, U>
                                                                    where W : IEntityView<T, U, V>
    {
        protected readonly IMapperView<T, U, V, W> _mapper;

        public Interactive(IMapperView<T, U, V, W> mapper)
        {
            _mapper = mapper;
        }
        public virtual W Clear(W presentation, IEntityLogic<T, U, V> logic)
        {
            presentation.Domain = logic.Clear();

            _mapper.Map(presentation);

            return presentation;
        }

        public virtual (Result result, W presentation) Load(W presentation, IEntityLogic<T, U, V> logic)
        {
            var load = logic.Load();
            presentation.Domain = load.domain;

            if (load.result.Success)
            {
                _mapper.Clear(presentation);
                _mapper.Map(presentation);
            }

            return (load.result, presentation);
        }
        public virtual (Result result, W presentation) Save(W presentation, IEntityLogic<T, U, V> logic)
        {
            var save = logic.Save();
            presentation.Domain = save.domain;

            if (save.result.Success)
            {
                _mapper.Map(presentation);
            }

            return (save.result, presentation);
        }
        public virtual (Result result, W presentation) Erase(W presentation, IEntityLogic<T, U, V> logic)
        {
            var erase = logic.Erase();
            presentation.Domain = erase.domain;

            if (erase.result.Success)
            {
                _mapper.Map(presentation);
            }

            return (erase.result, presentation);
        }

        public virtual (Result result, W presentation) Retrieve(IQueryLogic<T, U, V> logic, int maxdepth = 1, W presentation = default(W))
        {
            if (presentation == null)
            {
                presentation = (W)Activator.CreateInstance(typeof(W),
                        BindingFlags.CreateInstance |
                        BindingFlags.Public |
                        BindingFlags.Instance |
                        BindingFlags.OptionalParamBinding, null, null, CultureInfo.CurrentCulture);
            }

            var retrieve = logic.Retrieve(maxdepth, presentation.Domain);
            presentation.Domain = retrieve.domain;

            if (retrieve.result.Success)
            {
                _mapper.Clear(presentation);
                _mapper.Map(presentation);
            }

            return (retrieve.result, presentation);
        }
        public virtual (Result result, IEnumerable<W> presentations) List(IQueryLogic<T, U, V> logic, int maxdepth = 1, int top = 0, IList<W> presentations = null)
        {
            var enumeration = new List<W>();
            var iterator = (presentations ?? new List<W>()).GetEnumerator();

            var list = logic.List(maxdepth, top);
            foreach (var business in list.domains)
            {
                var presentation = iterator.MoveNext() ? iterator.Current : (W)Activator.CreateInstance(typeof(W),
                    BindingFlags.CreateInstance |
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.OptionalParamBinding, null, new object[] { maxdepth }, CultureInfo.CurrentCulture);

                presentation.Domain = business;

                _mapper.Clear(presentation);
                _mapper.Map(presentation);

                enumeration.Add(presentation);
            }

            return (list.result, enumeration);
        }
    }
}
