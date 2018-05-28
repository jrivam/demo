using library.Impl;
using library.Impl.Data.Model;
using library.Impl.Domain.Model;
using library.Interface.Data.Query;
using library.Interface.Domain.Model;
using library.Interface.Domain.Query;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Interface.Data.Model
{
    public interface IListEntityState<S, R, T, U, V>
        where T : IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>, IEntityLogic<T, U, V>
        where S : IQueryRepository<T, U>
        where R : IQueryLogic<T, U, V>
    {
        ListEntityTable<S, T, U> Datas { get; }

        U Data { get; }

        ListEntityState<S, R, T, U, V> Load(R query, int maxdepth = 1, int top = 0);
        ListEntityState<S, R, T, U, V> Load(IEnumerable<V> list);

        Result Save();
        Result Erase();
    }
}
