using Library.Interface.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Library.Impl.Entities
{
    public class ListEntity<T> : List<T>, IListEntity<T>
        where T : IEntity
    {
        public virtual IList<T> List
        {
            get
            {
                return this?.ToList();
            }
            set
            {
                if (this?.Count != 0 || value?.Count != 0)
                    this?.AddRange(value);
            }
        }

        public ListEntity(IList<T> list)
        {
            List = list;
        }
        public ListEntity()
            : this(new List<T>())
        {
        }

        public virtual IListEntity<T> Load(IEnumerable<T> list)
        {
            List = list?.ToList();

            return this;
        }

        public void ItemEdit(T oldentity, T newentity)
        {
        }
        public virtual bool ItemAdd(T entity)
        {
            if (entity.Id != null)
            {
                this.Add(entity);

                return true;
            }

            return false;
        }
        public virtual bool ItemRemove(T entity)
        {
            this.Remove(entity);

            return true;
        }
    }
}
