using library.Interface.Data.Model;
using library.Interface.Domain.Model;
using library.Interface.Entities;
using library.Interface.Presentation.Mapper;
using library.Interface.Presentation.Model;

namespace library.Impl.Presentation.Model
{
    public class InteractiveView<T, U, V, W> : Interactive<T, U, V, W>, IInteractiveView<T, U, V, W> 
        where T : IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>
        where W : IEntityView<T, U, V>
    {
        public InteractiveView(IMapperView<T, U, V, W> mapper)
            : base(mapper)
        {
        }
        public virtual W Clear(W presentation, IEntityLogic<T, U, V> entitylogic)
        {
            entitylogic.Clear();

            _mapper.Map(presentation);

            return presentation;
        }

        public virtual (Result result, W presentation) Load(W presentation, IEntityLogic<T, U, V> entitylogic, bool usedbcommand = false)
        {
            var load = entitylogic.Load(usedbcommand);

            if (load.result.Success)
            {
                _mapper.Clear(presentation);
                _mapper.Map(presentation);
            }

            return (load.result, presentation);
        }
        public virtual (Result result, W presentation) Save(W presentation, IEntityLogic<T, U, V> entitylogic, bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            var save = entitylogic.Save(useinsertdbcommand, useupdatedbcommand);

            if (save.result.Success)
            {
                _mapper.Map(presentation);
            }

            return (save.result, presentation);
        }
        public virtual (Result result, W presentation) Erase(W presentation, IEntityLogic<T, U, V> entitylogic, bool usedbcommand = false)
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
