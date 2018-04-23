using library.Interface.Data.Model;
using library.Interface.Domain.Model;
using library.Interface.Entities;
using library.Interface.Presentation;
using library.Interface.Presentation.Mapper;
using library.Interface.Presentation.Model;

namespace library.Impl.Presentation
{
    public class Interactive<T, U, V, W> : IInteractive<T, U, V, W> 
        where T : IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>
        where W : IEntityView<T, U, V>
    {
        protected readonly IMapperView<T, U, V, W> _mapper;

        public Interactive(IMapperView<T, U, V, W> mapper)
        {
            _mapper = mapper;
        }
    }
}
