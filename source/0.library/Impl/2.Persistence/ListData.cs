using Library.Impl.Entities;
using Library.Interface.Data;
using Library.Interface.Data.Query;
using Library.Interface.Data.Table;
using Library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Library.Impl.Data
{
    public class ListData<T, U> : List<U>, IListData<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        public virtual ListEntity<T> Entities
        {
            get
            {
                return new ListEntity<T>().Load(this?.Select(x => x.Entity));
            }
            set
            {
                value?.ForEach(x => this?.Add((U)Activator.CreateInstance(typeof(U),
                           BindingFlags.CreateInstance |
                           BindingFlags.Public |
                           BindingFlags.Instance |
                           BindingFlags.OptionalParamBinding,
                           null, new object[] { x },
                           CultureInfo.CurrentCulture)));
            }
        }

        public ListData(ListEntity<T> entities)
        {
            Entities = entities;
        }
        public ListData()
            : this(new ListEntity<T>())
        {
        }

        public virtual (Result result, ListData<T, U> list) LoadQuery<S>(S query, int maxdepth = 1, int top = 0)
            where S : IQueryData<T, U>
        {
            var list = query.SelectMultiple(maxdepth, top);

            return (list.result, Load(list.datas));
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
