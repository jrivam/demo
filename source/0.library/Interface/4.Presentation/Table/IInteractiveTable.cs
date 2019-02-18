using library.Impl;
using library.Interface.Data.Table;
using library.Interface.Domain.Query;
using library.Interface.Domain.Table;
using library.Interface.Entities;

namespace library.Interface.Presentation.Table
{
    public interface IInteractiveTable<T, U, V, W> 
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where V : ITableLogic<T, U>
        where W : ITableInteractive<T, U, V>
    {
        (Result result, W presentation) Load(W presentation, bool usedbcommand = false);
        (Result result, W presentation) LoadQuery(W presentation, int maxdepth = 1);
        (Result result, W presentation) Save(W presentation, bool useinsertdbcommand = false, bool useupdatedbcommand = false);
        (Result result, W presentation) Erase(W presentation, IQueryLogicMethods<T, U, V> query = null, bool usedbcommand = false);
    }
}
