using Library.Impl;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace Library.Interface.Persistence.Query
{
    public interface IQueryDataMethods<T, U> 
        where T : IEntity
        where U : ITableData<T, U>
    {
        (Result result, U data) SelectSingle(int maxdepth = 1, U data = default(U));
        (Result result, IEnumerable<U> datas) SelectMultiple(int maxdepth = 1, int top = 0, IListData<T, U> datas = null);

        (Result result, int rows) Update(IList<IColumnTable> columns, int maxdepth = 1);
        (Result result, int rows) Delete(int maxdepth = 1);
    }
}
