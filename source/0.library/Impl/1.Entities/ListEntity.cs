using library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

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
                value?.ForEach(x => this?.Add((T)Activator.CreateInstance(typeof(T),
                           BindingFlags.CreateInstance |
                           BindingFlags.Public |
                           BindingFlags.Instance |
                           BindingFlags.OptionalParamBinding,
                           null, new object[] { x },
                           CultureInfo.CurrentCulture)));
            }
        }

        public ListEntity(List<T> list)
        {
            list = List;
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
