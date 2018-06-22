using library.Impl;
using library.Impl.Data.Table;
using library.Impl.Domain.Table;
using library.Interface.Data.Query;
using library.Interface.Domain.Query;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Interface.Data.Table
{
    public interface IListEntityLogicProperties<S, R, T, U, V>
        where T : IEntity
        where U : ITableRepositoryProperties<T>
        where V : IEntityLogicProperties<T, U>, IEntityLogicMethods<T, U, V>
        where S : IQueryRepositoryMethods<T, U>
        where R : IQueryLogicMethods<T, U, V>
    {
        ListTableRepositoryProperties<S, T, U> Datas { get; set; }

        ListEntityLogicProperties<S, R, T, U, V> Load(R query, int maxdepth = 1, int top = 0);
        ListEntityLogicProperties<S, R, T, U, V> Load(IEnumerable<V> list);

        Result Save();
        Result Erase();
    }
}
