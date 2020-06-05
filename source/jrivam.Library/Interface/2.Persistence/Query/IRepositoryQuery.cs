using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace jrivam.Library.Interface.Persistence.Query
{
    public interface IRepositoryQuery<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        (Result result, U data) SelectSingle (IQueryData<T, U> query, int maxdepth = 1, U data = default(U));
        (Result result, IEnumerable<U> datas) Select (IQueryData<T, U> query, int maxdepth = 1, int top = 0, IListData<T, U> datas = null);
        (Result result, int rows) Update (IQueryData<T, U> query, IList<IColumnTable> tablecolumns, int maxdepth = 1);
        (Result result, int rows) Delete (IQueryData<T, U> query, int maxdepth = 1);        
    }
}
