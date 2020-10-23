using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Business.Loader;
using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Business.Query
{
    public partial class LogicQueryAsync<T, U, V> : ILogicQueryAsync<T, U, V> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : class, ITableDomain<T, U, V>
    {
        protected readonly ILogicAsync<T, U> _logicasync;

        protected readonly IDomainLoader _loader;

        public LogicQueryAsync(ILogicAsync<T, U> logicasync,
            IDomainLoader loader)
        {
            _logicasync = logicasync;

            _loader = loader;
        }

        public virtual async Task<(Result result, V domain)> RetrieveAsync(IQueryDomain<T, U, V> query,
            int maxdepth = 1,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            var list = await ListAsync(query,
                maxdepth, 1,
                connection,
                commandtimeout).ConfigureAwait(false);

            return (list.result, list.domains.FirstOrDefault());
        }

        public virtual async Task<(Result result, IEnumerable<V> domains)> ListAsync(IQueryDomain<T, U, V> query,
            int maxdepth = 1, int top = 0,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            var list = await _logicasync.ListAsync(query.Data,
                maxdepth, top,
                connection,
                commandtimeout).ConfigureAwait(false);

            if (list.result.Success)
            {
                var enumeration = new List<V>();

                if (list.datas != null)
                {
                    foreach (var data in list.datas)
                    {
                        var instance = Business.HelperTableLogic<T, U, V>.CreateDomain(data);

                        _loader.Load<T, U, V>(instance, maxdepth, 0);

                        instance.Changed = false;
                        instance.Deleted = false;

                        enumeration.Add(instance);
                    }
                }

                return (list.result, enumeration);
            }

            return (list.result, default(IList<V>));
        }
    }
}
