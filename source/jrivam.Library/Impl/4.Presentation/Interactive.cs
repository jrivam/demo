using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation;
using System.Collections.Generic;
using System.Data;

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

        public (Result result, V domain) Load(V domain, bool usedbcommand = false,
            IDbConnection connection = null)
        {
            return domain.Load(usedbcommand,
                connection);
        }
        public (Result result, V domain) LoadQuery(V domain, int maxdepth = 1,
            IDbConnection connection = null)
        {
            return domain.LoadQuery(maxdepth,
                connection);
        }

        public virtual (Result result, IEnumerable<V> domains) List(IQueryDomain<T, U, V> query,
            int maxdepth = 1, int top = 0,
            IDbConnection connection = null)
        {
            return query.List(maxdepth, top, 
                connection);
        }

        public (Result result, V domain) Save(V domain, bool useinsertdbcommand = false, bool useupdatedbcommand = false,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            return domain.Save(useinsertdbcommand, useupdatedbcommand,
                connection, transaction);
        }
        public (Result result, V domain) Erase(V domain, bool usedbcommand = false,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            return domain.Erase(usedbcommand,
                connection, transaction);
        }
    }
}
