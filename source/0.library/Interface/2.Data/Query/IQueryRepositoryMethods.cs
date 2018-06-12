using library.Impl;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Interface.Data.Query
{
    public interface IQueryRepositoryMethods<T, U> 
        where T : IEntity
        where U : IEntityRepositoryProperties<T>
    {
        (Result result, U data) SelectSingle(int maxdepth = 1, U data = default(U));
        (Result result, IEnumerable<U> datas) SelectMultiple(int maxdepth = 1, int top = 0, IList<U> datas = null);

        (Result result, int rows) Update(IList<IEntityColumn> columns, int maxdepth = 1);
        (Result result, int rows) Delete(int maxdepth = 1);
    }
}
