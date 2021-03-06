﻿using Library.Impl;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace Library.Interface.Business.Query
{
    public interface ILogicQuery<S, T, U, V> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where S : IQueryData<T, U>
    {
        (Result result, V domain) Retrieve(IQueryDomain<S, T, U, V> query, int maxdepth = 1, V domain = default(V));

        (Result result, IEnumerable<V> domains) List(IQueryDomain<S, T, U, V> query, int maxdepth = 1, int top = 0, IListDomain<T, U, V> domains = null);
    }
}
