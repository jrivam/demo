using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation.Raiser;
using jrivam.Library.Interface.Presentation.Table;

namespace jrivam.Library.Impl.Presentation.Table
{
    public class InteractiveTable<T, U, V, W> : InteractiveRaiser<T, U, V, W>, IInteractiveTable<T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        public InteractiveTable(IRaiser<T, U, V, W> raiser)
            : base(raiser)
        {
        }

        public virtual (Result result, W model) Load(W table, bool usedbcommand = false)
        {
            table.Status = "Loading...";

            var load = table.Domain.Load(usedbcommand);
            if (load.result.Success && load.domain != null)
            {
                table.Domain = load.domain;

                _raiser.Clear(table);
                Raise(table, 1);

                table.Status = string.Empty;

                return (load.result, table);
            }

            table.Status = load.result.FilteredAsText("/", x => x.category == (x.category & ResultCategory.OnlyErrors));

            return (load.result, default(W));
        }
        public virtual (Result result, W model) LoadQuery(W table, int maxdepth = 1)
        {
            table.Status = "Loading...";

            var loadquery = table.Domain.LoadQuery(maxdepth);
            if (loadquery.result.Success && loadquery.domain != null)
            {
                table.Domain = loadquery.domain;

                _raiser.Clear(table);
                Raise(table, maxdepth);

                table.Status = string.Empty;

                return (loadquery.result, table);
            }

            table.Status = loadquery.result.FilteredAsText("/", x => x.category == (x.category & ResultCategory.OnlyErrors));

            return (loadquery.result, default(W));
        }
        public virtual (Result result, W model) Save(W table, bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            table.Status = "Saving...";

            var save = table.Domain.Save(useinsertdbcommand, useupdatedbcommand);
            if (save.result.Success)
            {
                Raise(table);

                table.Status = string.Empty;

                return (save.result, table);
            }

            table.Status = save.result.FilteredAsText("/", x => x.category == (x.category & ResultCategory.OnlyErrors));

            return (save.result, default(W));
        }
        public virtual (Result result, W model) Erase(W table, bool usedbcommand = false)
        {
            table.Status = "Deleting...";

            var erase = table.Domain.Erase(usedbcommand);
            if (erase.result.Success)
            {
                //Raise(table);

                table.Status = string.Empty;

                return (erase.result, table);
            }

            table.Status = erase.result.FilteredAsText("/", x => x.category == (x.category & ResultCategory.OnlyErrors));

            return (erase.result, default(W));
        }
    }
}
