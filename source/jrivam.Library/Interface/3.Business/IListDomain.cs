using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace jrivam.Library.Interface.Business
{
    public interface IListDomain<T, U, V> : IList<V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        IListData<T, U> Datas { get; }
        
        IListDomain<T, U, V> Load(IEnumerable<V> list);
    }
}
