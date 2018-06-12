using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation.Table;

namespace library.Interface.Presentation.Mapper
{
    public interface IMapperInteractive<T, U, V, W> 
        where T : IEntity
        where U : IEntityRepositoryProperties<T>
        where V : IEntityLogicProperties<T, U>
        where W : IEntityInteractiveProperties<T, U, V>
    {
        W Clear(W presentation);
        W Map(W presentation);
    }
}