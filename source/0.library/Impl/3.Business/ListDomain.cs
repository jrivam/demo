﻿using Library.Impl.Data;
using Library.Interface.Data;
using Library.Interface.Data.Table;
using Library.Interface.Domain;
using Library.Interface.Domain.Table;
using Library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Library.Impl.Domain
{
    public class ListDomain<T, U, V> : List<V>, IListDomain<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        public virtual IListData<T, U> Datas
        {
            get
            {
                return new ListData<T, U>().Load(this?.Select(x => x.Data));
            }
            set
            {
                value?.ToList().ForEach(x => this?.Add((V)Activator.CreateInstance(typeof(V),
                           BindingFlags.CreateInstance |
                           BindingFlags.Public |
                           BindingFlags.Instance |
                           BindingFlags.OptionalParamBinding,
                           null, new object[] { x },
                           CultureInfo.CurrentCulture)));
            }
        }

        public ListDomain(IListData<T, U> datas)
        {
            Datas = datas;
        }
        public ListDomain()
            : this(new ListData<T, U>())
        {
        }

        public virtual IListDomain<T, U, V> Load(IEnumerable<V> list)
        {
            if (list != null)
            {
                this?.AddRange(list);
            }

            return this;
        }

        public virtual Result SaveAll()
        {
            var result = new Result() { Success = true };

            foreach (var domain in this)
            {
                result.Append(domain.Save().result);

                if (!result.Success) break;
            }

            return result;
        }
        public virtual Result EraseAll()
        {
            var result = new Result() { Success = true };

            foreach (var domain in this)
            {
                result.Append(domain.Erase().result);

                if (!result.Success) break;
            }

            return result;
        }
    }
}