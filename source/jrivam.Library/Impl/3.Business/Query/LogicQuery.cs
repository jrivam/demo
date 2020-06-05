using jrivam.Library.Impl.Persistence;
using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Business.Loader;
using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
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

        public virtual (Result result, V domain) Retrieve(IQueryDomain<T, U, V> query, int maxdepth = 1, V domain = default(V))
        {
            var list = List(query, maxdepth, 1, domain != null ? new ListDomain<T, U, V>() { domain } : null);

            return (list.result, list.domains.FirstOrDefault());
        }
        public virtual (Result result, IEnumerable<V> domains) List(IQueryDomain<T, U, V> query, int maxdepth = 1, int top = 0, IListDomain<T, U, V> domains = null)
        {
            var select = query.Data.Select(maxdepth, top, domains?.Datas ?? new ListData<T, U>());
            if (select.result.Success)
            {
                var enumeration = new List<V>();

                if (select.datas != null)
                {
                    foreach (var data in select.datas)
                    {
                        var instance = Business.HelperTableLogic<T, U, V>.CreateDomain(data);

                        _loader.Load<T, U, V>(instance, maxdepth, 0);

                        instance.Changed = false;
                        instance.Deleted = false;

                        enumeration.Add(instance);
                    }
                }

                return (select.result, enumeration);
            }

            return (select.result, default(IList<V>));
        }
    }
}
