using library.Impl;
using library.Interface.Business;
using library.Interface.Data;
using library.Interface.Domain;
using System.Collections.Generic;

namespace library.Interface.Presentation
{
    public interface IInteractive<T, U, V, W> where T : IEntity
                                                where U : IEntityTable<T>
                                                where V : IEntityState<T, U>
                                                where W : IEntityView<T, U, V>
    {
        W Clear(W presentation, IEntityLogic<T, U, V> logic, int maxdepth = 1);

        (Result result, W presentation) Load(W presentation, IEntityLogic<T, U, V> logic);
        (Result result, W presentation) Save(W presentation, IEntityLogic<T, U, V> logic);
        (Result result, W presentation) Erase(W presentation, IEntityLogic<T, U, V> logic);

        (Result result, W presentation) Retrieve(IQueryLogic<T, U, V> logic, int maxdepth = 1);
        (Result result, IEnumerable<W> presentations) List(IQueryLogic<T, U, V> logic, int maxdepth = 1, int top = 0);
    }
}
