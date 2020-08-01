using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace jrivam.Library.Impl.Business.Query
{
    public abstract class AbstractQueryDomain<T, U, V> : IQueryDomain<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        public IQueryData<T, U> Data { get; set; }

        protected readonly ILogicQuery<T, U, V> _logicquery;

        public AbstractQueryDomain(ILogicQuery<T, U, V> logicquery, 
            IQueryData<T, U> data)
        {
            _logicquery = logicquery;

            Data = data;
        }

        public virtual (Result result, V domain) Retrieve(int maxdepth = 1)
        {
            var retrieve = _logicquery.Retrieve(this, maxdepth);

            return retrieve;
        }
        public virtual (Result result, IEnumerable<V> domains) List(int maxdepth = 1, int top = 0)
        {
            var list = _logicquery.List(this, maxdepth, top);

            return list;
        }
    }
}
