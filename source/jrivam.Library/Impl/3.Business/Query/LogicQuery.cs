using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Business.Loader;
using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace jrivam.Library.Impl.Business.Query
{
    public class LogicQuery<T, U, V> : ILogicQuery<T, U, V> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : class, ITableDomain<T, U, V>
    {
        protected readonly ILogic<T, U> _logic;

        protected readonly IDomainLoader _loader;

        public LogicQuery(ILogic<T, U> logic,
            IDomainLoader loader)
        {
            _logic = logic;

            _loader = loader;
        }

        public virtual (Result result, V domain) Retrieve(IQueryDomain<T, U, V> query,
            int? commandtimeout = null,
            int maxdepth = 1,
            IDbConnection connection = null)
        {
            var list = List(query, 
                commandtimeout,
                maxdepth, 1, 
                connection);

            return (list.result, list.domains.FirstOrDefault());
        }
        public virtual (Result result, IEnumerable<V> domains) List(IQueryDomain<T, U, V> query,
            int? commandtimeout = null,
            int maxdepth = 1, int top = 0,
            IDbConnection connection = null)
        {
            var list = _logic.List(query.Data, 
                commandtimeout,
                maxdepth, top,
                connection);

            if (list.result.Success)
            {
                var enumeration = new List<V>();

                if (list.datas != null)
                {
                    foreach (var data in list.datas)
                    {
                        var instance = Business.HelperTableLogic<T, U, V>.CreateDomain(data);

                        _loader.Load<T, U, V>(instance, maxdepth, 0);

                        instance.Changed = false;
                        instance.Deleted = false;

                        enumeration.Add(instance);
                    }
                }

                return (list.result, enumeration);
            }

            return (list.result, default(IList<V>));
        }
    }
}
