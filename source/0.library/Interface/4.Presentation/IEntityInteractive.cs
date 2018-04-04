using library.Impl;
using library.Interface.Data;
using library.Interface.Domain;
using library.Interface.Entities;

namespace library.Interface.Presentation
{
    public interface IEntityInteractive<T, U, V, W> where T : IEntity
                                                    where U : IEntityTable<T>
                                                    where V : IEntityState<T, U>
                                                    where W : IEntityView<T, U, V>
    {
        W Clear();

        (Result result, W presentation) LoadIn(int maxdepth = 1);
        (Result result, W presentation) Load(bool usedbcommand = false);
        (Result result, W presentation) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false);
        (Result result, W presentation) Erase(bool usedbcommand = false);
    }
}
