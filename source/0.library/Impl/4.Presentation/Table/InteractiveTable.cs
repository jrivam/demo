using Library.Interface.Data.Table;
using Library.Interface.Domain.Table;
using Library.Interface.Entities;
using Library.Interface.Presentation.Raiser;
using Library.Interface.Presentation.Table;

namespace Library.Impl.Presentation.Table
{
    public class InteractiveTable<T, U, V, W> : Interactive<T, U, V, W>, IInteractiveTable<T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        public InteractiveTable(IRaiserInteractive<T, U, V, W> raiser)
            : base(raiser)
        {
        }

        public virtual (Result result, W presentation) Load(W table, bool usedbcommand = false)
        {
            var load = table.Domain.Load(usedbcommand);

            if (load.result.Success && load.domain != null)
            {
                //var presentation = _raiser.CreateInstance(load.domain, 1);

                _raiser.Clear(table, 1, 0);
                _raiser.Raise(table, 1, 0);

                _raiser.Extra(table, 1, 0);

                return (load.result, table);
            }

            return (load.result, default(W));
        }
        public virtual (Result result, W presentation) LoadQuery(W table, int maxdepth = 1)
        {
            var loadquery = table.Domain.LoadQuery(maxdepth);

            if (loadquery.result.Success && loadquery.domain != null)
            {
                //var presentation = _raiser.CreateInstance(loadquery.domain, 1);

                _raiser.Clear(table, maxdepth, 0);
                _raiser.Raise(table, maxdepth, 0);

                _raiser.Extra(table, maxdepth, 0);

                return (loadquery.result, table);
            }

            return (loadquery.result, default(W));
        }
        public virtual (Result result, W presentation) Save(W table, bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            var save = table.Domain.Save(useinsertdbcommand, useupdatedbcommand);

            if (save.result.Success)
            {
                //var presentation = _raiser.CreateInstance(save.domain, 1);

                _raiser.Raise(table);

                return (save.result, table);
            }

            return (save.result, default(W));
        }
        public virtual (Result result, W presentation) Erase(W table, bool usedbcommand = false)
        {
            var erase = table.Domain.Erase(usedbcommand);

            if (erase.result.Success)
            {
                //var presentation = _raiser.CreateInstance(erase.domain, 1);

                _raiser.Raise(table);

                return (erase.result, table);
            }

            return (erase.result, default(W));
        }
    }
}
