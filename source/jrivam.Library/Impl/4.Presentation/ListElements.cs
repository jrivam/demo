using jrivam.Library.Interface.Presentation;
using System.Collections.Generic;
using System.Linq;

namespace jrivam.Library.Impl.Presentation
{
    public class ListElements<T> : List<T>, IListElements<T>
        where T : IElement
    {
        public virtual T this[string name]
        {
            get
            {
                return this.SingleOrDefault(x => x.Name.ToLower() == name.ToLower());
            }
        }
    }
}
