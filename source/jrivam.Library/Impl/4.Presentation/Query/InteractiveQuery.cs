using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation;
using jrivam.Library.Interface.Presentation.Query;
using jrivam.Library.Interface.Presentation.Raiser;
using jrivam.Library.Interface.Presentation.Table;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Presentation.Query
{
    public partial class InteractiveQueryAsync<T, U, V, W> : IInteractiveQueryAsync<T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : class, ITableDomain<T, U, V>
        where W : class, ITableModel<T, U, V, W>
    {
        protected readonly IInteractiveAsync<T, U, V> _interactiveasync;

        protected readonly IModelRaiser _raiser;

        public InteractiveQueryAsync(IInteractiveAsync<T, U, V> interactiveasync,
            IModelRaiser raiser)
        {
            _interactiveasync = interactiveasync;

            _raiser = raiser;
        }

        public virtual async Task<(Result result, W model)> RetrieveAsync(IQueryModel<T, U, V, W> query,
            int maxdepth = 1,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            query.Status = "Retrieving...";

            var list = await ListAsync(query,
                maxdepth, 1,
                connection,
                commandtimeout).ConfigureAwait(false);

            query.Status = list.result.GetMessagesAsString();

            return (list.result, list.models.FirstOrDefault());
        }

        public virtual async Task<(Result result, IEnumerable<W> models)> ListAsync(IQueryModel<T, U, V, W> query,
            int maxdepth = 1, int top = 0,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            query.Status = "Listing...";

            var list = await _interactiveasync.ListAsync(query.Domain,
                maxdepth, top,
                connection,
                commandtimeout).ConfigureAwait(false);
            if (list.result.Success)
            {
                var enumeration = new List<W>();

                if (list.domains != null)
                {
                    foreach (var domain in list.domains)
                    {
                        var instance = Presentation.HelperTableInteractive<T, U, V, W>.CreateModel(domain, maxdepth);

                        _raiser.Raise<T, U, V, W>(instance, maxdepth);

                        instance.Status = string.Empty;

                        enumeration.Add(instance);
                    }
                }

                query.Status = string.Empty;

                return (list.result, enumeration);
            }

            query.Status = list.result.GetMessagesAsString();

            return (list.result, default(IList<W>));
        }
    }
}
