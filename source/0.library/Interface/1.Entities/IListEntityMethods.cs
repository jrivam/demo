using library.Impl.Entities;
using System.Collections.Generic;

namespace library.Interface.Entities
{
    public interface IListEntityMethods<T>
        where T : IEntity
    {
        ListEntity<T> Load(IEnumerable<T> list);
    }
}
