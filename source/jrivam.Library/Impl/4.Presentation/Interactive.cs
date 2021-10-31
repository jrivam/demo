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

        public (Result result, V domain) Load(V domain, 
            bool usedbcommand = false,
            int? commandtimeout = null,
            IDbConnection connection = null)
        {
            return domain.Load(usedbcommand,
                commandtimeout,
                connection);
        }
        public (Result result, V domain) LoadQuery(V domain,
            int? commandtimeout = null, 
            int maxdepth = 1,
            IDbConnection connection = null)
        {
            return domain.LoadQuery(commandtimeout,
                maxdepth,
                connection);
        }

        public virtual (Result result, IEnumerable<V> domains) List(IQueryDomain<T, U, V> query,
            int? commandtimeout = null, 
            int maxdepth = 1, int top = 0,
            IDbConnection connection = null)
        {
            return query.List(commandtimeout,
                maxdepth, top, 
                connection);
        }

        public (Result result, V domain) Save(V domain, 
            bool useinsertdbcommand = false, bool useupdatedbcommand = false,
            int? commandtimeout = null,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            return domain.Save(useinsertdbcommand, useupdatedbcommand,
                commandtimeout,
                connection, transaction);
        }
        public (Result result, V domain) Erase(V domain, 
            bool usedbcommand = false,
            int? commandtimeout = null, 
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            return domain.Erase(usedbcommand,
                commandtimeout,
                connection, transaction);
        }
    }
}
