using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation;
using jrivam.Library.Interface.Presentation.Raiser;
using jrivam.Library.Interface.Presentation.Table;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Presentation.Table
{
    public partial class InteractiveTableAsync<T, U, V, W> : IInteractiveTableAsync<T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        protected readonly IInteractiveAsync<T, U, V> _interactiveasync;

        protected readonly IModelRaiser _raiser;

        public InteractiveTableAsync(IInteractiveAsync<T, U, V> interactiveasync,
            IModelRaiser raiser)
        {
            _interactiveasync = interactiveasync;

            _raiser = raiser;
        }

        public virtual async Task<(Result result, W model)> LoadAsync(W model,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            model.Status = "Loading...";

            var load = await _interactiveasync.LoadAsync(model.Domain,
                connection,
                commandtimeout).ConfigureAwait(false);
            if (load.result.Success && load.domain != null)
            {
                model.Domain = load.domain;

                _raiser.Raise<T, U, V, W>(model, 1);

                model.Status = string.Empty;

                return (load.result, model);
            }

            model.Status = load.result.GetMessagesAsString();

            return (load.result, default(W));
        }

        public virtual async Task<(Result result, W model)> LoadQueryAsync(W model,
            int maxdepth = 1,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            model.Status = "Loading...";

            var loadquery = await _interactiveasync.LoadQueryAsync(model.Domain,
                maxdepth,
                connection,
                commandtimeout).ConfigureAwait(false);
            if (loadquery.result.Success && loadquery.domain != null)
            {
                model.Domain = loadquery.domain;

                _raiser.Raise<T, U, V, W>(model, maxdepth);

                model.Status = string.Empty;

                return (loadquery.result, model);
            }

            model.Status = loadquery.result.GetMessagesAsString();

            return (loadquery.result, default(W));
        }

        public virtual async Task<(Result result, W model)> SaveAsync(W model,
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            model.Status = "Saving...";

            var save = await _interactiveasync.SaveAsync(model.Domain,
                connection, transaction,
                commandtimeout).ConfigureAwait(false);
            if (save.result.Success)
            {
                _raiser.Raise<T, U, V, W>(model);

                model.Status = string.Empty;

                return (save.result, model);
            }

            model.Status = save.result.GetMessagesAsString();

            return (save.result, default(W));
        }

        public virtual async Task<(Result result, W model)> EraseAsync(W model,
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            model.Status = "Deleting...";

            var erase = await _interactiveasync.EraseAsync(model.Domain,
                connection, transaction,
                commandtimeout).ConfigureAwait(false);
            if (erase.result.Success)
            {
                model.Status = string.Empty;

                return (erase.result, model);
            }

            model.Status = erase.result.GetMessagesAsString();

            return (erase.result, default(W));
        }
    }
}
