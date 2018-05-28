using library.Interface.Data.Model;
using library.Interface.Data.Query;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Impl.Data.Model
{
    public class ListEntityTable<S, T, U> : List<U>, IListEntityTable<S, T, U>
        where T : IEntity
        where U : IEntityTable<T>
        where S : IQueryRepository<T, U>
    {
        public virtual IList<T> Entities
        {
            get
            {
                var list = new List<T>();
                this.ForEach(x => list.Add(x.Entity));
                return list;
            }
        }

        public ListEntityTable()
        {
        }

        public virtual ListEntityTable<S, T, U> Load(S query, int maxdepth = 1, int top = 0)
        {
            return Load(query.SelectMultiple(maxdepth, top).datas);
        }
        public virtual ListEntityTable<S, T, U> Load(IEnumerable<U> list)
        {
            this.AddRange(list);

            return this;
        }
    }
}
