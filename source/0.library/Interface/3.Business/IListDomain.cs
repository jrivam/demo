using Library.Impl;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence;
using Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace Library.Interface.Business
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
