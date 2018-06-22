using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace library.Impl.Data.Table
{
    public class ListTableRepositoryProperties<S, T, U> : List<U>, IListTableRepositoryProperties<S, T, U>
        where T : IEntity
        where U : ITableRepositoryProperties<T>
        where S : IQueryRepositoryMethods<T, U>
    {
        public virtual List<T> Entities
        {
            get
            {
                var list = new List<T>();
                this.ForEach(x => list.Add(x.Entity));
                return list;
            }
            set
            {
                value?.ForEach(x => this.Add((U)Activator.CreateInstance(typeof(U),
                           BindingFlags.CreateInstance |
                           BindingFlags.Public |
                           BindingFlags.Instance |
                           BindingFlags.OptionalParamBinding, null, new object[] { x }, CultureInfo.CurrentCulture)));
            }
        }

        public ListTableRepositoryProperties()
        {
        }

        public virtual ListTableRepositoryProperties<S, T, U> Load(S query, int maxdepth = 1, int top = 0)
        {
            return Load(query.SelectMultiple(maxdepth, top).datas);
        }
        public virtual ListTableRepositoryProperties<S, T, U> Load(IEnumerable<U> list)
        {
            this.AddRange(list);

            return this;
        }
    }
}
