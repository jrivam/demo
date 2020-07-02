using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation;
using jrivam.Library.Interface.Presentation.Raiser;
using jrivam.Library.Interface.Presentation.Table;

namespace jrivam.Library.Impl.Presentation.Table
{
    public class InteractiveTable<T, U, V, W> : IInteractiveTable<T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        protected readonly IInteractive<T, U, V> _interactive;

        protected readonly IModelRaiser _raiser;

        public InteractiveTable(IInteractive<T, U, V> interactive,
            IModelRaiser raiser)
        {
            _interactive = interactive;

            _raiser = raiser;
        }

        public virtual (Result result, W model) Load(W model, bool usedbcommand = false)
        {
            model.Status = "Loading...";

            var load = _interactive.Load(model.Domain, usedbcommand);
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
        public virtual (Result result, W model) LoadQuery(W model, int maxdepth = 1)
        {
            model.Status = "Loading...";

            var loadquery = _interactive.LoadQuery(model.Domain, maxdepth);
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
        
        public virtual (Result result, W model) Save(W model, bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            model.Status = "Saving...";

            var save = _interactive.Save(model.Domain, useupdatedbcommand);
            if (save.result.Success)
            {
                _raiser.Raise<T, U, V, W>(model);

                model.Status = string.Empty;

                return (save.result, model);
            }

            model.Status = save.result.GetMessagesAsString();

            return (save.result, default(W));
        }
        public virtual (Result result, W model) Erase(W model, bool usedbcommand = false)
        {
            model.Status = "Deleting...";

            var erase = _interactive.Erase(model.Domain, usedbcommand);
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
