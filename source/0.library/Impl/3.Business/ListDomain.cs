using Library.Impl.Data;
using Library.Interface.Data.Query;
using Library.Interface.Data.Table;
using Library.Interface.Domain;
using Library.Interface.Domain.Query;
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
        public virtual ListData<T, U> Datas
        {
            get
            {
                return new ListData<T, U>().Load(this?.Select(x => x.Data));
            }
            set
            {
                value?.ForEach(x => this?.Add((V)Activator.CreateInstance(typeof(V),
                           BindingFlags.CreateInstance |
                           BindingFlags.Public |
                           BindingFlags.Instance |
                           BindingFlags.OptionalParamBinding,
                           null, new object[] { x },
                           CultureInfo.CurrentCulture)));
            }
        }

        public ListDomain(ListData<T, U> datas)
        {
            Datas = datas;
        }
        public ListDomain()
            : this(new ListData<T, U>())
        {
        }

        public virtual (Result result, ListDomain<T, U, V> list) LoadQuery<R, S>(R query, int maxdepth = 1, int top = 0)
            where S : IQueryData<T, U>
            where R : IQueryDomain<S, T, U, V>
        {
            var list = query.List(maxdepth, top);

            return (list.result, Load(list.domains));
        }

        public virtual ListDomain<T, U, V> Load(IEnumerable<V> list)
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
