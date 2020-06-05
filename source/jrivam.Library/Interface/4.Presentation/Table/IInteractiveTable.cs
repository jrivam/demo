using jrivam.Library.Impl;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Presentation.Query;

namespace jrivam.Library.Interface.Presentation.Table
{
    public interface IInteractiveTable<T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        (Result result, W model) Load(W model, bool usedbcommand = false);
        (Result result, W model) LoadQuery(W model, IQueryModel<T, U, V, W> query, int maxdepth = 1);
        (Result result, W model) Save(W model, bool useinsertdbcommand = false, bool useupdatedbcommand = false);
        (Result result, W model) Erase(W model, bool usedbcommand = false);
    }
}
