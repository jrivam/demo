using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Presentation
{
    public partial class InteractiveAsync<T, U, V> : IInteractiveAsync<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        public InteractiveAsync()
        {
        }

        public virtual async Task<(Result result, V domain)> LoadAsync(V domain,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            return await domain.LoadAsync(connection,
                commandtimeout).ConfigureAwait(false);
        }

        public virtual async Task<(Result result, V domain)> LoadQueryAsync(V domain,
            int maxdepth = 1,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            return await domain.LoadQueryAsync(maxdepth,
                connection,
                commandtimeout).ConfigureAwait(false);
        }

        public virtual async Task<(Result result, IEnumerable<V> domains)> ListAsync(IQueryDomain<T, U, V> query,
            int maxdepth = 1, int top = 0,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            return await query.ListAsync(maxdepth, top,
                connection,
                commandtimeout).ConfigureAwait(false);
        }

        public virtual async Task<(Result result, V domain)> SaveAsync(V domain,
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            return await domain.SaveAsync(connection, transaction,
                commandtimeout).ConfigureAwait(false);
        }

        public virtual async Task<(Result result, V domain)> EraseAsync(V domain,
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            return await domain.EraseAsync(connection, transaction,
                commandtimeout).ConfigureAwait(false);
        }
    }
}
