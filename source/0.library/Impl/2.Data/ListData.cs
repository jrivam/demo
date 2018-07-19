﻿using library.Impl.Entities;
using library.Interface.Data;
using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace library.Impl.Data
{
    public class ListData<S, T, U> : List<U>, IListDataMethods<S, T, U>, IListData<T, U>
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where S : IQueryRepositoryMethods<T, U>
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

        public ListData()
        {
        }

        public virtual ListData<S, T, U> Load(S query, int maxdepth = 1, int top = 0)
        {
            return Load(query.SelectMultiple(maxdepth, top).datas);
        }
        public virtual ListData<S, T, U> Load(IEnumerable<U> list)
        {
            if (list != null)
            {
                this?.AddRange(list);
            }

            return this;
        }
    }
}