using Library.Interface.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace Library.Impl.Persistence
{
    public class ListColumns<T> : List<T>, IListColumns<T>
        where T : IColumn
    {
        public virtual T this[string reference]
        {
            get
            {
                return this.SingleOrDefault(x => x.Description.Reference.ToLower() == reference.ToLower());
            }
        }
    }
}
