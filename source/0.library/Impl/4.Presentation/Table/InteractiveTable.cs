using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;
using Library.Interface.Presentation.Raiser;
using Library.Interface.Presentation.Table;
using System;
using System.Linq;

namespace Library.Impl.Presentation.Table
{
    public class InteractiveTable<T, U, V, W> : Interactive<T, U, V, W>, IInteractiveTable<T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        public InteractiveTable(IRaiser<T, U, V, W> raiser)
            : base(raiser)
        {
        }

        public virtual (Result result, W presentation) Load(W table, bool usedbcommand = false)
        {
            table.Status = "Loading...";

            var load = table.Domain.Load(usedbcommand);

            if (load.result.Success && load.domain != null)
            {
                table.Domain = load.domain;

                _raiser.Clear(table);

                _raiser.Raise(table, 1, 0);
                _raiser.RaiseX(table, 1, 0);

                table.Status = string.Empty;

                return (load.result, table);
            }

            table.Status = String.Join("/", load.result.Messages.Where(x => x.category == ResultCategory.Error || x.category == ResultCategory.Exception).ToArray()).Replace(Environment.NewLine, string.Empty);

            return (load.result, default(W));
        }
        public virtual (Result result, W presentation) LoadQuery(W table, int maxdepth = 1)
        {
            table.Status = "Loading...";

            var loadquery = table.Domain.LoadQuery(maxdepth);

            if (loadquery.result.Success && loadquery.domain != null)
            {
                table.Domain = loadquery.domain;

                _raiser.Clear(table);

                _raiser.Raise(table, maxdepth, 0);
                _raiser.RaiseX(table, maxdepth, 0);

                table.Status = string.Empty;

                return (loadquery.result, table);
            }

            table.Status = String.Join("/", loadquery.result.Messages.Where(x => x.category == ResultCategory.Error || x.category == ResultCategory.Exception).ToArray()).Replace(Environment.NewLine, string.Empty);

            return (loadquery.result, default(W));
        }
        public virtual (Result result, W presentation) Save(W table, bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            table.Status = "Saving...";

            var save = table.Domain.Save(useinsertdbcommand, useupdatedbcommand);

            if (save.result.Success)
            {
                _raiser.Raise(table);
                _raiser.RaiseX(table);

                table.Status = string.Empty;

                return (save.result, table);
            }

            table.Status = String.Join("/", save.result.Messages.Where(x => x.category == ResultCategory.Error || x.category == ResultCategory.Exception).ToArray()).Replace(Environment.NewLine, string.Empty);

            return (save.result, default(W));
        }
        public virtual (Result result, W presentation) Erase(W table, bool usedbcommand = false)
        {
            table.Status = "Deleting...";

            var erase = table.Domain.Erase(usedbcommand);

            if (erase.result.Success)
            {
                _raiser.Raise(table);
                _raiser.RaiseX(table);

                table.Status = string.Empty;

                return (erase.result, table);
            }

            table.Status = String.Join("/", erase.result.Messages.Where(x => x.category == ResultCategory.Error || x.category == ResultCategory.Exception).ToArray()).Replace(Environment.NewLine, string.Empty);

            return (erase.result, default(W));
        }
    }
}
