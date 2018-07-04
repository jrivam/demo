using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation.Table;

namespace library.Interface.Presentation.Mapper
{
    public interface IMapperInteractive<T, U, V, W> 
        where T : IEntity
        where U : ITableRepositoryProperties<T>
        where V : IEntityLogicProperties<T, U>
        where W : IEntityInteractiveProperties<T, U, V>
    {
        W Clear(W presentation, int maxdepth = 1, int depth = 0);
        W Raise(W presentation);
    }
}