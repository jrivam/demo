using System.Collections.Generic;

namespace Library.Interface.Entities
{
    public interface IListEntity<T> : IList<T>
        where T : IEntity
    {
        IList<T> List { get; set; }

        IListEntity<T> Load(IEnumerable<T> list);

        void ItemEdit(T oldentity, T newentity);
        bool ItemAdd(T entity);
        bool ItemRemove(T entity);
    }
}
