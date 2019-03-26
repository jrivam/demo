﻿using Library.Impl;
using Library.Interface.Data.Table;
using Library.Interface.Entities;
using System.Collections.Generic;

namespace Library.Interface.Data.Query
{
    public interface IQueryDataMethods<T, U> 
        where T : IEntity
        where U : ITableData<T, U>
    {
        (Result result, U data) SelectSingle(int maxdepth = 1);
        (Result result, IEnumerable<U> datas) SelectMultiple(int maxdepth = 1, int top = 0);

        (Result result, int rows) Update(IList<IColumnTable> columns, int maxdepth = 1);
        (Result result, int rows) Delete(int maxdepth = 1);
    }
}
