using jrivam.Library.Interface.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace jrivam.Library.Impl.Persistence
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
