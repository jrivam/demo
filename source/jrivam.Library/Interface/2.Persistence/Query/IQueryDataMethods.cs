using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace jrivam.Library.Interface.Persistence.Query
{
    public interface IQueryDataMethods<T, U> 
        where T : IEntity
        where U : ITableData<T, U>
    {
        (Result result, U data) SelectSingle(int maxdepth = 1);
        (Result result, IEnumerable<U> datas) Select(int maxdepth = 1, int top = 0);

        (Result result, int rows) Update(IList<IColumnTable> columns, int maxdepth = 1);
        (Result result, int rows) Delete(int maxdepth = 1);
    }
}
