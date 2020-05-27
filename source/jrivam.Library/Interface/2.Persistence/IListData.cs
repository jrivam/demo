using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace jrivam.Library.Interface.Persistence
{
    public interface IListData<T, U> : IList<U>, IListDataEdit<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        ICollection<T> Entities { get; }

        IListData<T, U> Load(IEnumerable<U> list);
    }
}
