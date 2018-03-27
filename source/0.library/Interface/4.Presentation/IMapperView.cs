using library.Interface.Data;
using library.Interface.Domain;
using library.Interface.Entities;

namespace library.Interface.Presentation
{
    public interface IMapperView<T, U, V, W> where T : IEntity
                                            where U : IEntityTable<T>
                                            where V : IEntityState<T, U>
                                            where W : IEntityView<T, U, V>
    {
        W Clear(W presentation);
        W Map(W presentation);
    }
}