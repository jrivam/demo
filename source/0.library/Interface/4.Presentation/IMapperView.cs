using library.Interface.Business;
using library.Interface.Data;
using library.Interface.Domain;

namespace library.Interface.Presentation
{
    public interface IMapperView<T, U, V, W> where T : IEntity
                                            where U : IEntityTable<T>
                                            where V : IEntityState<T, U>
                                            where W : IEntityView<T, U, V>
    {
        W Clear(W presentation, int maxdepth = 1, int depth = 0);
        W Map(W presentation, int maxdepth = 1, int depth = 0);
    }
}