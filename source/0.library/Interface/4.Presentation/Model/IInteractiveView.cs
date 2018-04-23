using library.Impl;
using library.Interface.Data.Model;
using library.Interface.Domain.Model;
using library.Interface.Entities;

namespace library.Interface.Presentation.Model
{
    public interface IInteractiveView<T, U, V, W> 
        where T : IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>
        where W : IEntityView<T, U, V>
    {
        W Clear(W presentation, IEntityLogic<T, U, V> entitylogic);

        (Result result, W presentation) Load(W presentation, IEntityLogic<T, U, V> entitylogic, bool usedbcommand = false);
        (Result result, W presentation) Save(W presentation, IEntityLogic<T, U, V> entitylogic, bool useinsertdbcommand = false, bool useupdatedbcommand = false);
        (Result result, W presentation) Erase(W presentation, IEntityLogic<T, U, V> entitylogic, bool usedbcommand = false);
    }
}
