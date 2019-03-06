using library.Interface.Data;
using System.Collections.Generic;
using System.Linq;

namespace library.Impl.Data
{
    public class ListColumns<T> : List<T>
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
