using library.Impl;
using library.Interface.Data.Table;
using library.Interface.Entities;

namespace library.Interface.Domain.Table
{
    public interface ILogicTable<T, U, V> 
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where V : ITableLogic<T, U>
    {
        (Result result, V domain) Load(V domain, bool usedbcommand = false);
        (Result result, V domain) LoadQuery(V domain, int maxdepth = 1);
        (Result result, V domain) Save(V domain, bool useinsertdbcommand = false, bool useupdatedbcommand = false);
        (Result result, V domain) Erase(V domain, bool usedbcommand = false);
    }
}
