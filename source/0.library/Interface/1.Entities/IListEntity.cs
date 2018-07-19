using System.Collections.Generic;

namespace library.Interface.Entities
{
    public interface IListEntity<T> : IList<T>
        where T : IEntity
    {
        List<T> List { get; set; }
    }
}
