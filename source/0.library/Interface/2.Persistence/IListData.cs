using Library.Impl.Data;
using Library.Interface.Data.Table;
using Library.Interface.Entities;
using System.Collections.Generic;

namespace Library.Interface.Data
{
    public interface IListData<T, U> : IList<U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        IListEntity<T> Entities { get; set; }

        ListData<T, U> Load(IEnumerable<U> list);
    }
}
