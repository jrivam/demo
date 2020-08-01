﻿using jrivam.Library.Impl;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation.Table;
using System.Collections.Generic;

namespace jrivam.Library.Interface.Presentation.Query
{
    public interface IInteractiveQuery<T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        (Result result, W model) Retrieve(IQueryModel<T, U, V, W> query, int maxdepth = 1);
        (Result result, IEnumerable<W> models) List(IQueryModel<T, U, V, W> query, int maxdepth = 1, int top = 0);
    }
}
