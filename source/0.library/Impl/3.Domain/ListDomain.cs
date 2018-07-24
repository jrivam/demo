using library.Impl.Data;
using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Domain;
using library.Interface.Domain.Query;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace library.Impl.Domain
{
    public class ListDomain<S, R, T, U, V> : List<V>, IListDomain<S, T, U, V>, IListDomainMethods<S, R, T, U, V>
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where V : ITableLogic<T, U>, ITableLogicMethods<T, U, V>
        where S : IQueryRepositoryMethods<T, U>
        where R : IQueryLogicMethods<T, U, V>
    {
        public virtual ListData<S, T, U> Datas
        {
            get
            {
                return new ListData<S, T, U>().Load(this?.Select(x => x.Data));
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

        public ListDomain()
        {
        }

        public virtual ListDomain<S, R, T, U, V> Load(R query, int maxdepth = 1, int top = 0)
        {
            return Load(query?.List(maxdepth, top).domains);
        }
        public virtual ListDomain<S, R, T, U, V> Load(IEnumerable<V> list)
        {
            if (list != null)
            {
                this?.AddRange(list);
            }

            return this;
        }

        public virtual Result Save()
        {
            var save = new Result() { Success = true };

            foreach (var domain in this)
            {
                save.Append(domain.Save().result);

                if (!save.Success) break;
            }

            return save;
        }

        public virtual Result Erase()
        {
            var erase = new Result() { Success = true };

            foreach (var domain in this)
            {
                erase.Append(domain.Erase().result);

                if (!erase.Success) break;
            }

            return erase;
        }
    }
}
