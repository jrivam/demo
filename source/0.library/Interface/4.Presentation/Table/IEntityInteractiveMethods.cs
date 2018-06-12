using library.Impl;
using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Domain.Query;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation.Query;

namespace library.Interface.Presentation.Table
{
    public interface IEntityInteractiveMethods<S, R, Q, T, U, V, W>
        where S : IQueryRepositoryMethods<T, U>
        where R : IQueryLogicMethods<T, U, V>
        where Q : IQueryInteractiveMethods<T, U, V, W>
        where T : IEntity
        where U : IEntityRepositoryProperties<T>
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
