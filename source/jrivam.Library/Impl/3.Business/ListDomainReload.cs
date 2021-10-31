using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Table;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Impl.Business
{
    public partial class ListDomainReload<S, T, U, V> : ListDomainEdit<T, U, V>, IListDomainReloadAsync<T, U, V>, IListDomainQuery<S, T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : class, ITableDomain<T, U, V>
        where S : IQueryDomain<T, U, V>
    {
        public S Query { get; set; }
        protected int _maxdepth = 1;

        public ListDomainReload(S query,
            IListDataEdit<T, U> datas = null,
            int maxdepth = 1)
            : base(datas)
        {
            Query = query;
            _maxdepth = maxdepth;
        }

        public virtual async Task<(Result result, IListDomain<T, U, V> domains)> RefreshAsync(int top = 0,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            var list = await Query.ListAsync(_maxdepth, top,
                connection,
                commandtimeout).ConfigureAwait(false);

            if (list.result.Success)
            {
                var load = Load(list.domains, true);

                return (list.result, load);
            }

            return (list.result, default(IListDomain<T, U, V>));
        }

        public virtual async Task<Result> RefreshEraseAllAsync(int top = 0,
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            await RefreshAsync(top, connection, commandtimeout);
            return await EraseAllAsync(connection, transaction, commandtimeout);
        }
    }
}
