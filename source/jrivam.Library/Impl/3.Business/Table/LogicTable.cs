using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Business.Loader;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Business.Table
{
    public partial class LogicTableAsync<T, U, V> : ILogicTableAsync<T, U, V> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        protected readonly ILogicAsync<T, U> _logicasync;

        protected readonly IDomainLoader _loader;

        public LogicTableAsync(ILogicAsync<T, U> logicasync,
            IDomainLoader loader)
        {
            _logicasync = logicasync;

            _loader = loader;
        }

        public virtual async Task<(Result result, V domain)> LoadQueryAsync(V domain,
            int maxdepth = 1,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            var loadquery = await _logicasync.LoadQueryAsync(domain.Data,
                maxdepth,
                connection,
                commandtimeout).ConfigureAwait(false);
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

        public virtual async Task<(Result result, V domain)> LoadAsync(V domain,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            var load = await _logicasync.LoadAsync(domain.Data,
                connection,
                commandtimeout).ConfigureAwait(false);
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

        public virtual async Task<(Result result, V domain)> SaveAsync(V domain,
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            if (domain.Changed)
            {
                var validate = domain.Validate();

                if (validate.Success)
                {
                    var save = await _logicasync.SaveAsync(domain.Data,
                        connection, transaction,
                        commandtimeout).ConfigureAwait(false);

                    domain.Changed = !save.result.Success;

                    return (save.result, domain);
                }

                return (validate, default(V));
            }

            return (new Result(
                new ResultMessage()
                {
                    Category = ResultCategory.Information,
                    Name = $"{this.GetType().Name}.{nameof(SaveAsync)}",
                    Description = $"No changes to persist in {domain.Data.Description.DbName} with Id {domain.Data.Entity.Id}."
                }
                    ), default(V));
        }

        public virtual async Task<(Result result, V domain)> EraseAsync(V domain,
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            if (!domain.Deleted)
            {
                var erase = await _logicasync.EraseAsync(domain.Data,
                    connection, transaction,
                    commandtimeout).ConfigureAwait(false);

                domain.Deleted = erase.result.Success;

                return (erase.result, domain);
            }

            return (new Result(
                new ResultMessage()
                {
                    Category = ResultCategory.Information,
                    Name = $"{this.GetType().Name}.{nameof(EraseAsync)}",
                    Description = $"{domain.Data.Description.DbName} with Id {domain.Data.Entity.Id} already deleted."
                }
                    ), default(V));
        }
    }
}
