using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation;
using System.Collections.Generic;

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
        public (Result result, V domain) LoadQuery(V domain, int maxdepth = 1)
        {
            return domain.LoadQuery(maxdepth);
        }

        public virtual (Result result, IEnumerable<V> domains) List(IQueryDomain<T, U, V> query,
            int maxdepth = 1, int top = 0, IListDomain<T, U, V> domains = null)
        {
            return query.List(maxdepth, top, domains);
        }

        public (Result result, V domain) Save(V domain, bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            return domain.Save(useinsertdbcommand, useupdatedbcommand);
        }
        public (Result result, V domain) Erase(V domain, bool usedbcommand = false)
        {
            return domain.Erase(usedbcommand);
        }
    }
}
