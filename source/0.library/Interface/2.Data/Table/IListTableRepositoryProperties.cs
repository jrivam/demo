using library.Impl.Data.Table;
using library.Interface.Data.Query;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Interface.Data.Table
{
    public interface IListTableRepositoryProperties<S, T, U>
        where T : IEntity
        where U : ITableRepositoryProperties<T>
        where S : IQueryRepositoryMethods<T, U>
    {
        List<T> Entities { get; set; }

        ListTableRepositoryProperties<S, T, U> Load(S query, int maxdepth = 1, int top = 0);
        ListTableRepositoryProperties<S, T, U> Load(IEnumerable<U> list);
    }
}
