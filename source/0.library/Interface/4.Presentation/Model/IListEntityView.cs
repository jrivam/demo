using library.Impl;
using library.Impl.Domain.Model;
using library.Interface.Data.Query;
using library.Interface.Domain.Model;
using library.Interface.Domain.Query;
using library.Interface.Entities;
using library.Interface.Presentation.Model;
using library.Interface.Presentation.Query;
using System.Collections.Generic;

namespace library.Interface.Data.Model
{
    public interface IListEntityView<S, R, Q, T, U, V, W>
        where T : IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>, IEntityLogic<T, U, V>
        where W : IEntityView<T, U, V>, IEntityInteractive<T, U, V, W>
        where S : IQueryRepository<T, U>
        where R : IQueryLogic<T, U, V>
        where Q : IQueryInteractive<T, U, V, W>
    {
        ListEntityState<S, R, T, U, V> Domains { get; }

        V Domain { get; }

        ListEntityView<S, R, Q, T, U, V, W> Load(Q query, int maxdepth = 1, int top = 0);
        ListEntityView<S, R, Q, T, U, V, W> Load(IEnumerable<W> list);

        Result Save();
        Result Erase();
    }
}
