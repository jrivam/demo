using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Impl.Business
{
    public class ListDomainReload<S, T, U, V> : ListDomainEdit<T, U, V>, IListDomainQuery<S, T, U, V>, IListDomainReload<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : class, ITableDomain<T, U, V>
        where S : IQueryDomain<T, U, V>
    {
        public S Query { get; set; }
        protected int _maxdepth = 1;

        public ListDomainReload(S query,
            IListDataEdit<T, U> datas = null,
            int maxdepth = 1)
            : base(datas)
        {
            Query = query;
            _maxdepth = maxdepth;
        }

        public virtual (Result result, IListDomain<T, U, V> domains) Refresh(int top = 0)
        {
            var list = Query.List(_maxdepth, top);

            if (list.result.Success)
            {
                this.Clear();

                var load = Load(list.domains);

                return (list.result, load);
            }

            return (list.result, null);
        }
    }
}
