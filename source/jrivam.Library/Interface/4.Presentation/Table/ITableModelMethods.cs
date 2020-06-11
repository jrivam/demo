using jrivam.Library.Impl;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Interface.Presentation.Table
{
    public interface ITableModelMethods<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        (Result result, W model) Load(bool usedbcommand = false);
        (Result result, W model) LoadQuery(int maxdepth = 1);
        (Result result, W model) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false);
        (Result result, W model) Erase(bool usedbcommand = false);
    }
}
