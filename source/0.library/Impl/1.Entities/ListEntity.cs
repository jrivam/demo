using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Impl.Entities
{
    public class ListEntity<T> : List<T>, IListEntity<T>, IListEntityMethods<T>
        where T : IEntity
    {
        public virtual List<T> List
        {
            get
            {
                var list = new List<T>();
                this?.ForEach(x => list.Add(x));
                return list;
            }
            set
            {
                value?.ForEach(x => this?.Add(x));
            }
        }

        public ListEntity(List<T> list)
        {
            List = list;
        }
        public ListEntity()
            : this(new List<T>())
        {
        }

        public virtual ListEntity<T> Load(IEnumerable<T> list)
        {
            if (list != null)
            {
                this?.AddRange(list);
            }

            return this;
        }
    }
}
