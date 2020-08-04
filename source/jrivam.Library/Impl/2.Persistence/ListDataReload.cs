using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Impl.Persistence
{
    public class ListDataReload<S, T, U> : ListDataEdit<T, U>, IListDataReload<T, U>, IListDataQuery<S, T, U> 
        where T : IEntity
        where U : ITableData<T, U>
        where S : IQueryData<T, U>
    {
        public S Query { get; set; }
        protected int _maxdepth = 1;

        public ListDataReload(S query, 
            ICollection<T> entities = null,
            int maxdepth = 1)
            : base(entities)
        {
            Query = query;
            _maxdepth = maxdepth;
        }

        public virtual (Result result, IListData<T, U> datas) Refresh(int top = 0,
            IDbConnection connection = null)
        {
            var select = Query.Select(_maxdepth, top,
                connection);

            if (select.result.Success)
            {
                var load = Load(select.datas, true);

                return (select.result, load);
            }

            return (select.result, default(IListData<T, U>));
        }
    }
}
