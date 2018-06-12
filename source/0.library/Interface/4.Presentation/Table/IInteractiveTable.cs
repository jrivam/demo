using library.Impl;
using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;

namespace library.Interface.Presentation.Table
{
    public interface IInteractiveTable<T, U, V, W> 
        where T : IEntity
        where U : IEntityRepositoryProperties<T>
        where V : IEntityLogicProperties<T, U>
        where W : IEntityInteractiveProperties<T, U, V>
    {
        W Clear(W presentation, IEntityLogicMethods<T, U, V> entitylogic);

        (Result result, W presentation) Load(W presentation, IEntityLogicMethods<T, U, V> entitylogic, bool usedbcommand = false);
        (Result result, W presentation) LoadQuery(W presentation, IEntityLogicMethods<T, U, V> entitylogic, int maxdepth = 1);
        (Result result, W presentation) Save(W presentation, IEntityLogicMethods<T, U, V> entitylogic, bool useinsertdbcommand = false, bool useupdatedbcommand = false);
        (Result result, W presentation) Erase(W presentation, IEntityLogicMethods<T, U, V> entitylogic, bool usedbcommand = false);
    }
}
