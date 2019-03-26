using Library.Interface.Data;
using System.Collections.Generic;
using System.Linq;

namespace Library.Impl.Data
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
