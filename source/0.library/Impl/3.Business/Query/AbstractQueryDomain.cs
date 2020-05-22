using Library.Interface.Business;
using Library.Interface.Business.Query;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace Library.Impl.Business.Query
{
    public abstract class AbstractQueryDomain<S, T, U, V> : IQueryDomain<S, T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where S : IQueryData<T, U>
    {
        public virtual S Data { get; protected set; }

        public virtual IColumnQuery this[string name]
        {
            get
            {
                return Data[name];
            }
        }

        protected readonly ILogicQuery<S, T, U, V> _logic;

        public AbstractQueryDomain(S data, ILogicQuery<S, T, U, V> logic)
        {
            Data = data;

            _logic = logic;
        }

        public virtual (Result result, V domain) Retrieve(int maxdepth = 1, V domain = default(V))
        {
            var retrieve = _logic.Retrieve(this, maxdepth, domain);

            return retrieve;
        }
        public virtual (Result result, IEnumerable<V> domains) List(int maxdepth = 1, int top = 0, IListDomain<T, U, V> domains = null)
        {
            var list = _logic.List(this, maxdepth, top, domains);

            return list;
        }
    }
}
