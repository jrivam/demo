using Library.Impl.Entities;
using Library.Interface.Persistence;
using Library.Interface.Persistence.Table;
using Library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Library.Impl.Persistence
{
    public class ListData<T, U> : List<U>, IListData<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        public virtual IListEntity<T> Entities
        {
            get
            {
                return new ListEntity<T>().Load(this?.Select(x => x.Entity));
            }
            set
            {
                value?.ToList().ForEach(x => this?.Add((U)Activator.CreateInstance(typeof(U),
                           BindingFlags.CreateInstance |
                           BindingFlags.Public |
                           BindingFlags.Instance |
                           BindingFlags.OptionalParamBinding,
                           null, new object[] { x },
                           CultureInfo.CurrentCulture)));
            }
        }

        public ListData(IListEntity<T> entities)
        {
            Entities = entities;
        }
        public ListData()
            : this(new ListEntity<T>())
        {
        }

        public virtual ListData<T, U> Load(IEnumerable<U> list)
        {
            if (list != null)
            {
                this?.AddRange(list);
            }

            return this;
        }
    }
}
