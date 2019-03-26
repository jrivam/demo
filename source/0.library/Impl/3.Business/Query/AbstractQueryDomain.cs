using Library.Interface.Data.Query;
using Library.Interface.Data.Table;
using Library.Interface.Domain.Query;
using Library.Interface.Domain.Table;
using Library.Interface.Entities;
using System.Collections.Generic;

namespace Library.Impl.Domain.Query
{
    public abstract class AbstractQueryDomain<S, T, U, V> : IQueryDomain<S, T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where S : IQueryData<T, U>, new()
    {
        public virtual S Data { get; protected set; }

        public virtual IColumnQuery this[string reference]
        {
            get
            {
                return Data[reference];
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
            return _logic.Retrieve(this, maxdepth, domain);
        }
        public virtual (Result result, IEnumerable<V> domains) List(int maxdepth = 1, int top = 0, IList<V> domains = null)
        {
            return _logic.List(this, maxdepth, top, domains);
        }
    }
}
