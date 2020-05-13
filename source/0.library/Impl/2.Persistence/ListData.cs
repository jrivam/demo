using Library.Impl.Entities;
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
        protected IListEntity<T> _entities;
        public virtual IListEntity<T> Entities
        {
            get
            {
                return _entities;
            }
            set
            {
                if (_entities != value)
                {
                    _entities = value;
                    _entities?.ToList().ForEach(x => this.Add(Persistence.HelperRepository<T, U>.CreateInstance(x)));
                }
            }
        }

        public ListData(IListEntity<T> entities)
        {
            Entities = entities;
        }
        public ListData()
            : this(new ListEntity<T>())
        {
        }

        public virtual ListData<T, U> Load(IEnumerable<U> list)
        {
            if (list != null)
            {
                this?.AddRange(list);
                _entities = new ListEntity<T>(this?.Select(x => x.Entity).ToList());
            }

            return this;
        }

        public virtual void ItemEdit(U olddata, U newdata)
        {
            Entities.ItemEdit(olddata.Entity, newdata.Entity);
        }
        public virtual bool ItemAdd(U data)
        {
            if (Entities.ItemAdd(data.Entity))
            {
                this.Add(data);

                return true;
            }

            return false;
        }
        public virtual bool ItemRemove(U data)
        {
            if (Entities.ItemRemove(data.Entity))
            {
                this.Remove(data);

                return true;
            }

            return false;
        }
    }
}
