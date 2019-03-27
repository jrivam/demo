using Library.Impl;
using Library.Interface.Data;
using Library.Interface.Data.Table;
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
        IListData<T, U> Datas { get; set; }
        
        IListDomain<T, U, V> Load(IEnumerable<V> list);

        Result SaveAll();
        Result EraseAll();
    }
}
