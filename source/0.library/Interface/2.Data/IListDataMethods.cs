using library.Impl;
using library.Impl.Data;
using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Interface.Data
{
    public interface IListDataMethods<S, T, U>
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where S : IQueryRepositoryMethods<T, U>
    {
        (Result result, ListData<S, T, U> list) Load(S query, int maxdepth = 1, int top = 0);
        ListData<S, T, U> Load(IEnumerable<U> list);
    }
}
