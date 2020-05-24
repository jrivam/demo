using Library.Impl.Persistence;
using Library.Interface.Business;
using Library.Interface.Business.Loader;
using Library.Interface.Business.Query;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace Library.Impl.Business.Query
{
    public class LogicQuery<S, T, U, V> : LogicLoader<T, U, V>, ILogicQuery<S, T, U, V> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where S : IQueryData<T, U>
    {
        public LogicQuery(ILoader<T, U, V> loader)
            : base(loader)
        {
        }

        public virtual (Result result, V domain) Retrieve(IQueryDomain<S, T, U, V> query, int maxdepth = 1, V domain = default(V))
        {
            var selectsingle = query.Data.SelectSingle(maxdepth, domain != null ? domain.Data : default(U));
            if (selectsingle.result.Success)
            {
                if (selectsingle.data != null)
                {
                    var instance = Business.HelperTableLogic<T, U, V>.CreateDomain(selectsingle.data);

                    Load(instance, maxdepth);

                    instance.Changed = false;
                    instance.Deleted = false;

                    return (selectsingle.result, instance);
                }
            }

            return (selectsingle.result, default(V));
        }
        public virtual (Result result, IEnumerable<V> domains) List(IQueryDomain<S, T, U, V> query, int maxdepth = 1, int top = 0, IListDomain<T, U, V> domains = null)
        {
            var selectmultiple = query.Data.Select(maxdepth, top, domains?.Datas ?? new ListData<T, U>());
            if (selectmultiple.result.Success)
            {
                var enumeration = new List<V>();

                if (selectmultiple.datas != null)
                {
                    foreach (var instance in LoadDatas(selectmultiple.datas, maxdepth))
                    {
                        instance.Changed = false;
                        instance.Deleted = false;

                        enumeration.Add(instance);
                    }
                }

                return (selectmultiple.result, enumeration);
            }

            return (selectmultiple.result, default(IList<V>));
        }
    }
}
