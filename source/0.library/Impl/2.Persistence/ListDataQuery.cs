using Library.Interface.Entities;
using Library.Interface.Persistence;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace Library.Impl.Persistence
{
    public class ListDataQuery<S, T, U> : ListData<T, U>, IListDataQuery<S, T, U>, IListDataRefresh<T, U>
        where T : IEntity
        where U : ITableData<T, U>
        where S : IQueryData<T, U>
    {
        public S Query { get; set; }
        protected int _maxdepth = 1;

        public ListDataQuery(ICollection<T> entities,
            S query,
            int maxdepth = 1)
            : base(entities)
        {
            Query = query;
            _maxdepth = maxdepth;
        }

        public virtual (Result result, IListData<T, U> datas) Refresh(int top = 0)
        {
            var select = Query.Select(_maxdepth, top);

            if (select.result.Success)
            {
                this.Clear();

                var load = Load(select.datas);

                return (select.result, load);
            }

            return (select.result, null);
        }
    }
}
