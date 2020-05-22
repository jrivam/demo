using Library.Interface.Entities;
using Library.Interface.Persistence;
using Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Linq;

namespace Library.Impl.Persistence
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

        public ListData()
        {
        }
        public ListData(ICollection<T> entities)
            : this()
        {
            _entities = entities;
            _entities?.ToList()?.ForEach(x => this.Add(Persistence.HelperTableRepository<T, U>.CreateData(x)));
        }

        public virtual IListData<T, U> Load(IEnumerable<U> datas)
        {
            if (datas != null)
            {
                this?.AddRange(datas);

                _entities?.Clear();
                this.ToList()?.ForEach(x => _entities?.Add(x.Entity));
            }

            return this;
        }

        public virtual void ItemEdit(U olddata, U newdata)
        {
        }
        public virtual bool ItemAdd(U data)
        {
            if (data != null)
            {
                if (data.Entity.Id != null)
                {
                    this.Add(data);

                    return true;
                }
            }

            return false;
        }
        public virtual bool ItemRemove(U data)
        {
            if (data != null)
            {
                this.Remove(data);

                return true;
            }

            return true;
        }
    }
}
