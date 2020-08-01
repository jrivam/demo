using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Business.Loader;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Data;

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

        public virtual (Result result, V domain) Load(V domain, bool usedbcommand = false,
            IDbConnection connection = null)
        {
            var load = _logic.Load(domain.Data, usedbcommand,
                connection);
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
        public virtual (Result result, V domain) LoadQuery(V domain, int maxdepth = 1,
            IDbConnection connection = null)
        {
            var loadquery = _logic.LoadQuery(domain.Data, maxdepth,
                connection);
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

        public virtual (Result result, V domain) Save(V domain, bool useinsertdbcommand = false, bool useupdatedbcommand = false,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            if (domain.Changed)
            {
                var validate = domain.Validate();

                if (validate.Success)
                {
                    var save = _logic.Save(domain.Data, useinsertdbcommand, useupdatedbcommand,
                        connection, transaction);

                    domain.Changed = !save.result.Success;

                    return (save.result, domain);
                }

                return (validate, default(V));
            }

            return (new Result(
                new ResultMessage()
                        {
                            Category = ResultCategory.Information,
                            Name = $"{this.GetType().Name}.{nameof(Save)}",
                            Description =  $"No changes to persist in {domain.Data.Description.DbName} with Id {domain.Data.Entity.Id}."
                        }
                    ), default(V));
        }
        public virtual (Result result, V domain) Erase(V domain, bool usedbcommand = false,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            if (!domain.Deleted)
            {
                var erase = _logic.Erase(domain.Data, usedbcommand,
                    connection, transaction);

                domain.Deleted = erase.result.Success;

                return (erase.result, domain);
            }

            return (new Result(
                new ResultMessage()
                        {
                            Category = ResultCategory.Information,
                            Name = $"{this.GetType().Name}.{nameof(Erase)}",
                            Description =  $"{domain.Data.Description.DbName} with Id {domain.Data.Entity.Id} already deleted."
                        }
                    ), default(V));
        }
    }
}
