using Library.Impl;
using Library.Impl.Data;
using Library.Impl.Domain;
using Library.Interface.Data.Query;
using Library.Interface.Data.Table;
using Library.Interface.Domain.Query;
using Library.Interface.Domain.Table;
using Library.Interface.Entities;
using System.Collections.Generic;

namespace Library.Interface.Domain
{
    public interface IListDomain<T, U, V> : IList<V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        ListData<T, U> Datas { get; set; }

        (Result result, ListDomain<T, U, V> list) LoadQuery<R, S>(R query, int maxdepth = 1, int top = 0)
            where S : IQueryData<T, U>
            where R : IQueryDomain<S, T, U, V>;
        
        ListDomain<T, U, V> Load(IEnumerable<V> list);

        Result SaveAll();
        Result EraseAll();
    }
}
