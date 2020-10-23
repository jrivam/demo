using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Business.Query
{
    public abstract partial class AbstractQueryDomain<T, U, V> : IQueryDomain<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        public IQueryData<T, U> Data { get; set; }

        protected readonly ILogicQueryAsync<T, U, V> _logicqueryasync;

        public AbstractQueryDomain(ILogicQueryAsync<T, U, V> logicqueryasync, 
            IQueryData<T, U> data)
        {
            _logicqueryasync = logicqueryasync;

            Data = data;
        }

        public virtual async Task<(Result result, V domain)> RetrieveAsync(int maxdepth = 1,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            var retrieve = await _logicqueryasync.RetrieveAsync(this,
                maxdepth,
                connection,
                commandtimeout).ConfigureAwait(false);

            return retrieve;
        }

        public virtual async Task<(Result result, IEnumerable<V> domains)> ListAsync(int maxdepth = 1, int top = 0,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            var list = await _logicqueryasync.ListAsync(this,
                maxdepth, top,
                connection,
                commandtimeout).ConfigureAwait(false);

            return list;
        }
    }
}
