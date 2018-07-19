using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;

namespace library.Interface.Presentation
{
    public interface IInteractive<T, U, V> 
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where V : ITableLogic<T, U>
    {
    }
}
