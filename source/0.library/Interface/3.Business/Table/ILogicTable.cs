using Library.Impl;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;

namespace Library.Interface.Business.Table
{
    public interface ILogicTable<T, U, V> : ILogic<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        (Result result, V domain) Load(V table, bool usedbcommand = false);
        (Result result, V domain) LoadQuery(V table, int maxdepth = 1);
        (Result result, V domain) Save(V table, bool useinsertdbcommand = false, bool useupdatedbcommand = false);
        (Result result, V domain) Erase(V table, bool usedbcommand = false);
    }
}
