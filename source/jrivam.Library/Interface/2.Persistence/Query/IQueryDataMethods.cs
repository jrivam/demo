﻿using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Interface.Persistence.Query
{
    public interface IQueryDataMethods<T, U> 
        where T : IEntity
        where U : ITableData<T, U>
    {
        (Result result, U data) SelectSingle(int? commandtimeout = null, int maxdepth = 1, IDbConnection connection = null);
        (Result result, IEnumerable<U> datas) Select(int? commandtimeout = null, int maxdepth = 1, int top = 0, IDbConnection connection = null);

        (Result result, int rows) Update(IList<IColumnTable> columns, int? commandtimeout = null, int maxdepth = 1, IDbConnection connection = null, IDbTransaction transaction = null);
        (Result result, int rows) Delete(int? commandtimeout = null, int maxdepth = 1, IDbConnection connection = null, IDbTransaction transaction = null);
    }
}
