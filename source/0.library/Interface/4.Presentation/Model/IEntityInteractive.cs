using library.Impl;
using library.Interface.Data.Model;
using library.Interface.Domain.Model;
using library.Interface.Entities;

namespace library.Interface.Presentation.Model
{
    public interface IEntityInteractive<T, U, V, W> 
        where T : IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>
        where W : IEntityView<T, U, V>
    {
        W Clear();

        (Result result, W presentation) LoadQuery();
        (Result result, W presentation) Load(bool usedbcommand = false);
        (Result result, W presentation) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false);
        (Result result, W presentation) Erase(bool usedbcommand = false);
    }
}
