using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation;

namespace library.Impl.Presentation
{
    public class Interactive<T, U, V> : IInteractive<T, U, V> 
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where V : ITableLogic<T, U>
    {
        public Interactive()
        {
        }
    }
}
