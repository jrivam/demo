using library.Impl.Data.Model;
using library.Interface.Data.Model;
using library.Interface.Data.Query;
using library.Interface.Domain.Model;
using library.Interface.Domain.Query;
using library.Interface.Entities;
using System.Collections.Generic;
using System.Linq;

namespace library.Impl.Domain.Model
{
    public class ListEntityState<S, R, T, U, V> : List<V>, IListEntityState<S, R, T, U, V>
        where T : IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>, IEntityLogic<T, U, V>
        where S : IQueryRepository<T, U>
        where R : IQueryLogic<T, U, V>
    {
        public virtual ListEntityTable<S, T, U> Datas
        {
            get
            {
                var list = new ListEntityTable<S, T, U>();
                this.ForEach(x => list.Add(x.Data));
                return list;
                //return new ListEntityTable<S, T, U>().Load(this.Select(x => x.Data).Cast<U>());
            }
        }
        public virtual U Data
        {
            get
            {
                return this.Count > 0 ? this[0].Data : default(U);
                //return new ListEntityTable<S, T, U>().Load(this.Select(x => x.Data).Cast<U>());
            }
        }

        public ListEntityState()
        {
        }

        public virtual ListEntityState<S, R, T, U, V> Load(R query, int maxdepth = 1, int top = 0)
        {
            return Load(query.List(maxdepth, top).domains);
        }
        public virtual ListEntityState<S, R, T, U, V> Load(IEnumerable<V> list)
        {
            this.AddRange(list);

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
