using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace jrivam.Library.Impl.Persistence
{
    public class ListDataReload<S, T, U> : ListDataEdit<T, U>, IListDataQuery<S, T, U>, IListDataReload<T, U>
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
