using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation;

namespace jrivam.Library.Impl.Presentation
{
    public class Interactive<T, U, V> : IInteractive<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        public Interactive()
        {
        }

        public (Result result, V domain) Load(V domain, bool usedbcommand = false)
        {
            return domain.Load(usedbcommand);
        }
        public (Result result, V domain) LoadQuery(IQueryDomain<T, U, V> query, V domain, int maxdepth = 1)
        {
            return domain.LoadQuery(query, maxdepth);
        }

        public (Result result, V domain) Save(V domain, bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            return domain.Save(useupdatedbcommand);
        }
        public (Result result, V domain) Erase(V domain, bool usedbcommand = false)
        {
            return domain.Erase(usedbcommand);
        }
    }
}
