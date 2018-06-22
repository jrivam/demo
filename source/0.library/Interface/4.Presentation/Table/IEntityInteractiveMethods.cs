using library.Impl;
using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;

namespace library.Interface.Presentation.Table
{
    public interface IEntityInteractiveMethods<T, U, V, W>
        where T : IEntity
        where U : ITableRepositoryProperties<T>
        where V : IEntityLogicProperties<T, U>
        where W : class, IEntityInteractiveProperties<T, U, V>
    {
        W Clear();

        (Result result, W presentation) Load(bool usedbcommand = false);
        (Result result, W presentation) LoadQuery(int maxdepth = 1);
        (Result result, W presentation) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false);
        (Result result, W presentation) Erase(bool usedbcommand = false);
    }
}
