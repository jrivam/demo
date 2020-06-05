﻿using jrivam.Library.Impl;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace jrivam.Library.Interface.Business.Query
{
    public interface ILogicQuery<T, U, V> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        (Result result, V domain) Retrieve(IQueryDomain<T, U, V> query, int maxdepth = 1, V domain = default(V));
        (Result result, IEnumerable<V> domains) List(IQueryDomain<T, U, V> query, int maxdepth = 1, int top = 0, IListDomain<T, U, V> domains = null);
    }
}
