using library.Impl;
using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;

namespace library.Interface.Presentation.Table
{
    public interface IInteractiveTable<T, U, V, W> 
        where T : IEntity
        where U : ITableRepositoryProperties<T>
        where V : ITableLogicProperties<T, U>
        where W : ITableInteractiveProperties<T, U, V>
    {
        W Clear(W presentation, ITableLogicMethods<T, U, V> entitylogic);

        (Result result, W presentation) Load(W presentation, ITableLogicMethods<T, U, V> entitylogic, bool usedbcommand = false);
        (Result result, W presentation) LoadQuery(W presentation, ITableLogicMethods<T, U, V> entitylogic, int maxdepth = 1);
        (Result result, W presentation) Save(W presentation, ITableLogicMethods<T, U, V> entitylogic, bool useinsertdbcommand = false, bool useupdatedbcommand = false);
        (Result result, W presentation) Erase(W presentation, ITableLogicMethods<T, U, V> entitylogic, bool usedbcommand = false);
    }
}
