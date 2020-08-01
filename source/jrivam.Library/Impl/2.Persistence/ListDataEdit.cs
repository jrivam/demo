using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace jrivam.Library.Impl.Persistence
{
    public class ListDataEdit<T, U> : ListData<T, U>, IListDataEdit<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        public ListDataEdit(ICollection<T> entities = null)
            : base(entities)
        {
        }

        public virtual void ItemModify(U olddata, U newdata)
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
