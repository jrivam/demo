using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation.Raiser;
using library.Interface.Presentation.Table;

namespace library.Impl.Presentation.Table
{
    public class InteractiveTable<T, U, V, W> : Interactive<T, U, V>, IInteractiveTable<T, U, V, W> 
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where V : ITableLogic<T, U>, ITableLogicMethods<T, U, V>
        where W : ITableInteractive<T, U, V>
    {
        protected readonly IRaiserInteractive<T, U, V, W> _raiser;

        public InteractiveTable(IRaiserInteractive<T, U, V, W> raiser)
            : base()
        {
            _raiser = raiser;
        }

        public virtual (Result result, W presentation) Load(W presentation, bool usedbcommand = false)
        {
            var load = presentation.Domain.Load(usedbcommand);

            if (load.result.Success && load.domain != null)
            {
                _raiser.Clear(presentation, 1, 0);
                _raiser.Raise(presentation, 1, 0);

                _raiser.Extra(presentation, 1, 0);

                return (load.result, presentation);
            }

            return (load.result, default(W));
        }
        public virtual (Result result, W presentation) LoadQuery(W presentation, int maxdepth = 1)
        {
            var loadquery = presentation.Domain.LoadQuery(maxdepth);

            if (loadquery.result.Success && loadquery.domain != null)
            {
                _raiser.Clear(presentation, maxdepth, 0);
                _raiser.Raise(presentation, maxdepth, 0);

                _raiser.Extra(presentation, maxdepth, 0);

                return (loadquery.result, presentation);
            }

            return (loadquery.result, default(W));
        }
        public virtual (Result result, W presentation) Save(W presentation, bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            var save = presentation.Domain.Save(useinsertdbcommand, useupdatedbcommand);

            if (save.result.Success)
            {
                _raiser.Raise(presentation);
            }

            return (save.result, presentation);
        }
        public virtual (Result result, W presentation) Erase(W presentation, bool usedbcommand = false)
        {
            var erase = presentation.Domain.Erase(usedbcommand);

            if (erase.result.Success)
            {
                _raiser.Raise(presentation);
            }

            return (erase.result, presentation);
        }
    }
}
