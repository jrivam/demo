using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation.Table;

namespace library.Interface.Presentation
{
    public interface IInteractive<T, U, V, W> 
        where T : IEntity
        where U : IEntityRepositoryProperties<T>
        where V : IEntityLogicProperties<T, U>
        where W : IEntityInteractiveProperties<T, U, V>
    {
    }
}
