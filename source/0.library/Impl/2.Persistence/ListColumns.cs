using Library.Interface.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace Library.Impl.Persistence
{
    public class ListColumns<T> : List<T>, IListColumns<T>
        where T : IColumn
    {
        public virtual T this[string name]
        {
            get
            {
                return this.SingleOrDefault(x => x.Description.Name.ToLower() == name.ToLower());
            }
        }
    }
}
