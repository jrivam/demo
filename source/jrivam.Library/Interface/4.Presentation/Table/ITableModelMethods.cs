using jrivam.Library.Impl;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Data;

namespace jrivam.Library.Interface.Presentation.Table
{
    public interface ITableModelMethods<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        (Result result, W model) Load(bool usedbcommand = false, IDbConnection connection = null);
        (Result result, W model) LoadQuery(int maxdepth = 1, IDbConnection connection = null);

        (Result result, W model) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false, IDbConnection connection = null, IDbTransaction transaction = null);
        (Result result, W model) Erase(bool usedbcommand = false, IDbConnection connection = null, IDbTransaction transaction = null);
    }
}
