using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace jrivam.Library.Impl.Persistence
{
    public class ListDataEdit<T, U> : List<U>, IListDataEdit<T, U>
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

        public ListDataEdit(ICollection<T> entities = null)
        {
            _entities = entities ?? new Collection<T>();
            _entities?.ToList()?.ForEach(x => this.Add(Persistence.HelperTableRepository<T, U>.CreateData(x)));
        }

        public virtual IListDataEdit<T, U> Load(IEnumerable<U> datas)
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
