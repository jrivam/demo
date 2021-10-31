using jrivam.Library.Impl;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Data;

namespace jrivam.Library.Interface.Presentation.Table
{
    public interface IInteractiveTable<T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        (Result result, W model) Load(W model, bool usedbcommand = false, int? commandtimeout = null, IDbConnection connection = null);
        (Result result, W model) LoadQuery(W model, int? commandtimeout = null, int maxdepth = 1, IDbConnection connection = null);

        (Result result, W model) Save(W model, bool useinsertdbcommand = false, bool useupdatedbcommand = false, int? commandtimeout = null, IDbConnection connection = null, IDbTransaction transaction = null);
        (Result result, W model) Erase(W model, bool usedbcommand = false, int? commandtimeout = null, IDbConnection connection = null, IDbTransaction transaction = null);
    }
}
