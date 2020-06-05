using jrivam.Library.Impl;
using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Interface.Presentation
{
    public interface IInteractive<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        (Result result, V domain) Load(V domain, bool usedbcommand = false);
        (Result result, V domain) LoadQuery(IQueryDomain<T, U, V> query, V domain, int maxdepth = 1);
        (Result result, V domain) Save(V domain, bool useinsertdbcommand = false, bool useupdatedbcommand = false);
        (Result result, V domain) Erase(V domain, bool usedbcommand = false);
    }
}
