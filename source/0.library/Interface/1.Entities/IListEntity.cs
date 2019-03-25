using Library.Impl.Entities;
using System.Collections.Generic;

namespace Library.Interface.Entities
{
    public interface IListEntity<T> : IList<T>
        where T : IEntity
    {
        List<T> List { get; set; }

        ListEntity<T> Load(IEnumerable<T> list);
    }
}
