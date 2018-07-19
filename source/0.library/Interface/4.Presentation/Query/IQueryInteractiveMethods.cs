using library.Impl;
using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation.Table;
using System.Collections.Generic;

namespace library.Interface.Presentation.Query
{
    public interface IQueryInteractiveMethods<T, U, V, W> 
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where V : ITableLogic<T, U>
        where W : ITableInteractive<T, U, V>
    {
        (Result result, W presentation) Retrieve(int maxdepth, W presentation = default(W));
        (Result result, IEnumerable<W> presentations) List(int maxdepth = 1, int top = 0, IList<W> presentations = null);
    }
}
