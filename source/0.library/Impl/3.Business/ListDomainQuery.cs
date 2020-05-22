using Library.Interface.Business;
using Library.Interface.Business.Query;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Table;

namespace Library.Impl.Business
{
    public class ListDomainQuery<R, S, T, U, V> : ListDomain<T, U, V>, IListDomainQuery<R, S, T, U, V>, IListDomainRefresh<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where S : IQueryData<T, U>
        where R : IQueryDomain<S, T, U, V>
    {
        public R Query { get; set; }
        protected int _maxdepth = 1;

        public ListDomainQuery(IListData<T, U> datas,
            R query,
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
