using library.Impl.Data.Table;
using library.Interface.Data.Query;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Interface.Data.Table
{
    public interface IListEntityRepositoryProperties<S, T, U>
        where T : IEntity
        where U : IEntityRepositoryProperties<T>
        where S : IQueryRepositoryMethods<T, U>
    {
        List<T> Entities { get; set; }

        ListEntityRepositoryProperties<S, T, U> Load(S query, int maxdepth = 1, int top = 0);
        ListEntityRepositoryProperties<S, T, U> Load(IEnumerable<U> list);
    }
}
