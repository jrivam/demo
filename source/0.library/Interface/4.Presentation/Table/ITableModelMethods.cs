using Library.Impl;
using Library.Interface.Data.Table;
using Library.Interface.Domain.Table;
using Library.Interface.Entities;

namespace Library.Interface.Presentation.Table
{
    public interface ITableModelMethods<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        (Result result, W presentation) Load(bool usedbcommand = false);
        (Result result, W presentation) LoadQuery(int maxdepth = 1);
        (Result result, W presentation) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false);
        (Result result, W presentation) Erase(bool usedbcommand = false);
    }
}
