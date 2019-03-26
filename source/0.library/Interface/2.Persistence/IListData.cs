using Library.Impl;
using Library.Impl.Data;
using Library.Impl.Entities;
using Library.Interface.Data.Query;
using Library.Interface.Data.Table;
using Library.Interface.Entities;
using System.Collections.Generic;

namespace Library.Interface.Data
{
    public interface IListData<T, U> : IList<U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        ListEntity<T> Entities { get; set; }

        (Result result, ListData<T, U> list) LoadQuery<S>(S query, int maxdepth = 1, int top = 0)
            where S : IQueryData<T, U>;
        ListData<T, U> Load(IEnumerable<U> list);
    }
}
