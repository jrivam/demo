using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation.Query;
using jrivam.Library.Interface.Presentation.Table;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Presentation.Query
{
    public abstract partial class AbstractQueryModel<T, U, V, W> : IQueryModel<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        public IQueryDomain<T, U, V> Domain { get; set; }

        public string Status { get; set; } = string.Empty;

        protected readonly IInteractiveQueryAsync<T, U, V, W> _interactivequeryasync;

        public AbstractQueryModel(IInteractiveQueryAsync<T, U, V, W> interactivequeryasync,
            IQueryDomain<T, U, V> domain)
        {
            _interactivequeryasync = interactivequeryasync;

            Domain = domain;
        }

        public virtual async Task<(Result result, W model)> RetrieveAsync(int maxdepth = 1,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            var retrieve = await _interactivequeryasync.RetrieveAsync(this,
                maxdepth,
                connection,
                commandtimeout).ConfigureAwait(false);

            return retrieve;
        }

        public virtual async Task<(Result result, IEnumerable<W> models)> ListAsync(int maxdepth = 1, int top = 0,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            var list = await _interactivequeryasync.ListAsync(this,
                maxdepth, top,
                connection,
                commandtimeout).ConfigureAwait(false);

            return list;
        }
    }
}
