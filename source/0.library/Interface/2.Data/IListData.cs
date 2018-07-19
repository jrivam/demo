using library.Impl.Entities;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Interface.Data
{
    public interface IListData<T, U> : IList<U>
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
    {
        ListEntity<T> Entities { get; set; }
    }
}
