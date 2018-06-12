using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation.Mapper;
using library.Interface.Presentation.Table;

namespace library.Impl.Presentation.Table
{
    public class InteractiveTable<T, U, V, W> : Interactive<T, U, V, W>, IInteractiveTable<T, U, V, W> 
        where T : IEntity
        where U : IEntityRepositoryProperties<T>
        where V : IEntityLogicProperties<T, U>
        where W : IEntityInteractiveProperties<T, U, V>
    {
        public InteractiveTable(IMapperInteractive<T, U, V, W> mapper)
            : base(mapper)
        {
        }
        public virtual W Clear(W presentation, IEntityLogicMethods<T, U, V> entitylogic)
        {
            entitylogic.Clear();

            _mapper.Map(presentation);

            return presentation;
        }

        public virtual (Result result, W presentation) Load(W presentation, IEntityLogicMethods<T, U, V> entitylogic, bool usedbcommand = false)
        {
            var load = entitylogic.Load(usedbcommand);

            if (load.result.Success && load.domain != null)
            {
                _mapper.Clear(presentation);
                _mapper.Map(presentation);

                return (load.result, presentation);
            }

            return (load.result, default(W));
        }
        public virtual (Result result, W presentation) LoadQuery(W presentation, IEntityLogicMethods<T, U, V> entitylogic, int maxdepth = 1)
        {
            var load = entitylogic.LoadQuery(maxdepth);

            if (load.result.Success && load.domain != null)
            {
                _mapper.Clear(presentation);
                _mapper.Map(presentation);

                return (load.result, presentation);
            }

            return (load.result, default(W));
        }
        public virtual (Result result, W presentation) Save(W presentation, IEntityLogicMethods<T, U, V> entitylogic, bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            var save = entitylogic.Save(useinsertdbcommand, useupdatedbcommand);

            if (save.result.Success)
            {
                _mapper.Map(presentation);
            }

            return (save.result, presentation);
        }
        public virtual (Result result, W presentation) Erase(W presentation, IEntityLogicMethods<T, U, V> entitylogic, bool usedbcommand = false)
        {
            var erase = entitylogic.Erase(usedbcommand);

            if (erase.result.Success)
            {
                _mapper.Map(presentation);
            }

            return (erase.result, presentation);
        }
    }
}
