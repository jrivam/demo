using library.Interface.Data.Model;
using library.Interface.Domain.Model;
using library.Interface.Entities;
using library.Interface.Presentation.Model;

namespace library.Interface.Presentation.Mapper
{
    public interface IMapperView<T, U, V, W> 
        where T : IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>
        where W : IEntityView<T, U, V>
    {
        W Clear(W presentation);
        W Map(W presentation);
    }
}