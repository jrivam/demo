﻿using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;
using Library.Interface.Presentation.Table;

namespace Library.Interface.Presentation.Raiser
{
    public interface IRaiser<T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        W Clear(W model);

        W Raise(W model, int maxdepth = 1, int depth = 0);
        W RaiseX(W model, int maxdepth = 1, int depth = 0);
    }
}