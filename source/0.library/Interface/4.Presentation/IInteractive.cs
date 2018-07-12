using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation.Table;

namespace library.Interface.Presentation
{
    public interface IInteractive<T, U, V, W> 
        where T : IEntity
        where U : ITableRepositoryProperties<T>
        where V : ITableLogicProperties<T, U>
        where W : ITableInteractiveProperties<T, U, V>
    {
    }
}
