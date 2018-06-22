using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation;
using library.Interface.Presentation.Mapper;
using library.Interface.Presentation.Table;

namespace library.Impl.Presentation
{
    public class Interactive<T, U, V, W> : IInteractive<T, U, V, W> 
        where T : IEntity
        where U : ITableRepositoryProperties<T>
        where V : IEntityLogicProperties<T, U>
        where W : IEntityInteractiveProperties<T, U, V>
    {
        protected readonly IMapperInteractive<T, U, V, W> _mapper;

        public Interactive(IMapperInteractive<T, U, V, W> mapper)
        {
            _mapper = mapper;
        }
    }
}
