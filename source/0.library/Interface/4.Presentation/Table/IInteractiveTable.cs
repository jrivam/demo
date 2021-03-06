﻿using Library.Impl;
using Library.Interface.Persistence.Table;
using Library.Interface.Business.Table;
using Library.Interface.Entities;

namespace Library.Interface.Presentation.Table
{
    public interface IInteractiveTable<T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        (Result result, W model) Load(W table, bool usedbcommand = false);
        (Result result, W model) LoadQuery(W table, int maxdepth = 1);
        (Result result, W model) Save(W table, bool useinsertdbcommand = false, bool useupdatedbcommand = false);
        (Result result, W model) Erase(W table, bool usedbcommand = false);
    }
}
