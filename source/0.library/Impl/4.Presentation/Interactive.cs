using library.Interface.Business;
using library.Interface.Data;
using library.Interface.Domain;
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
        public virtual W Clear(W presentation, IEntityLogic<T, U, V> logic, int maxdepth = 1)
        {
            presentation.Business = logic.Clear();

            _mapper.Map(presentation, maxdepth, 0);

            return presentation;
        }

        public virtual (Result result, W presentation) Load(W presentation, IEntityLogic<T, U, V> logic)
        {
            _mapper.Clear(presentation, 1, 0);

            var load = logic.Load();
            presentation.Business = load.business;

            if (load.result.Success && load.result.Passed)
            {
                _mapper.Map(presentation, 1, 0);
            }

            return (load.result, presentation);
        }
        public virtual (Result result, W presentation) Save(W presentation, IEntityLogic<T, U, V> logic)
        {
            var save = logic.Save();
            presentation.Business = save.business;

            _mapper.Map(presentation, 1, 0);

            return (save.result, presentation);
        }
        public virtual (Result result, W presentation) Erase(W presentation, IEntityLogic<T, U, V> logic)
        {
            var erase = logic.Erase();
            presentation.Business = erase.business;

            _mapper.Map(presentation, 1, 0);

            return (erase.result, presentation);
        }

        public virtual (Result result, W presentation) Retrieve(IQueryLogic<T, U, V> logic, int maxdepth = 1)
        {
            var presentation = (W)Activator.CreateInstance(typeof(W),
                    BindingFlags.CreateInstance |
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.OptionalParamBinding, null, null, CultureInfo.CurrentCulture);

            var retrieve = logic.Retrieve(maxdepth);
            presentation.Business = retrieve.business;

            _mapper.Map(presentation, maxdepth, 0);

            return (retrieve.result, presentation);
        }
        public virtual (Result result, IEnumerable<W> presentations) List(IQueryLogic<T, U, V> logic, int maxdepth, int top)
        {
            var presentations = new List<W>();

            var list = logic.List(maxdepth, top);
            foreach(var iterator in list.businesses)
            {
                var presentation = (W)Activator.CreateInstance(typeof(W),
                    BindingFlags.CreateInstance |
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.OptionalParamBinding, null, null, CultureInfo.CurrentCulture);
                presentation.Business = iterator;

                _mapper.Map(presentation, maxdepth, 0);

                presentations.Add(presentation);
            }

            return (list.result, presentations);
        }
    }
}
