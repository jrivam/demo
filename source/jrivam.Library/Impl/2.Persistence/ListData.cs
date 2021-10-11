using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace jrivam.Library.Impl.Persistence
{
    public class ListData<T, U> : List<U>, IListData<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        protected ICollection<T> _entities;
        public virtual ICollection<T> Entities
        {
            get
            {
                return _entities;
            }
        }

        public ListData(ICollection<T> entities = null)
        {
            _entities = entities ?? new Collection<T>();
            _entities?.ToList()?.ForEach(x => this.Add(Persistence.HelperTableRepository<T, U>.CreateData(x)));
        }

        public virtual IListData<T, U> Load(IEnumerable<U> datas, bool clear = false)
        {
            if (clear || datas == null)
            {
                this.Clear();
            }

            if (datas != null)
            {
                this.AddRange(datas);
            }

            _entities?.Clear();
            this.ForEach(x => _entities?.Add(x.Entity));

            return this;
        }
    }
}
