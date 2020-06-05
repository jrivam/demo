using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Business.Loader;
using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace jrivam.Library.Impl.Business.Table
{
    public class LogicTable<T, U, V> : ILogicTable<T, U, V> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        protected readonly ILogic<T, U> _logic;

        protected readonly IDomainLoader _loader;

        public LogicTable(ILogic<T, U> logic,
            IDomainLoader loader)
        {
            _logic = logic;

            _loader = loader;
        }

        public virtual (Result result, V domain) Load(V domain, bool usedbcommand = false)
        {
            var load = _logic.Load(domain.Data, usedbcommand);
            if (load.result.Success && load.data != null)
            {
                domain.Data = load.data;

                _loader.Load<T, U, V>(domain, 1);

                domain.Changed = false;
                domain.Deleted = false;

                return (load.result, domain);
            }

            return (load.result, default(V));
        }
        public virtual (Result result, V domain) LoadQuery(V domain, IQueryDomain<T, U, V> query, int maxdepth = 1)
        {
            var loadquery = _logic.LoadQuery(query.Data, domain.Data, maxdepth);
            if (loadquery.result.Success && loadquery.data != null)
            {
                domain.Data = loadquery.data;

                _loader.Load<T, U, V>(domain, maxdepth);

                domain.Changed = false;
                domain.Deleted = false;

                return (loadquery.result, domain);
            }

            return (loadquery.result, default(V));
        }

        public virtual (Result result, V domain) Save(V domain, bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            if (domain.Changed)
            {
                var validate = domain.Validate();

                if (validate.Success)
                {
                    var save = _logic.Save(domain.Data, useupdatedbcommand);

                    domain.Changed = !save.result.Success;

                    return (save.result, domain);
                }

                return (validate, default(V));
            }

            return (new Result() { Success = true, Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Information, nameof(Save), $"No changes to persist in {domain.Data.Description.DbName} with Id {domain.Data.Entity.Id}") } }, default(V));
        }
        public virtual (Result result, V domain) Erase(V domain, bool usedbcommand = false)
        {
            if (!domain.Deleted)
            {
                var erase = _logic.Erase(domain.Data, usedbcommand);

                domain.Deleted = erase.result.Success;

                return (erase.result, domain);
            }

            return (new Result() { Success = true, Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Information, nameof(Erase), $"{domain.Data.Description.DbName} with Id {domain.Data.Entity.Id} already deleted") } }, default(V));
        }
    }
}
