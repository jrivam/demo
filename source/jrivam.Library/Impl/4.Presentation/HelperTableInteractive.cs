using Autofac;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation.Table;
using System.Collections.Generic;
using System.Linq;

namespace jrivam.Library.Impl.Presentation
{
    public class HelperTableInteractive<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        public static W CreateModel(V domain, int maxdepth = 1)
        {
            return AutofacConfiguration.Container.Resolve<W>(new TypedParameter(typeof(V), domain),
                        new NamedParameter("maxdepth", maxdepth));
        }
        public static IEnumerable<W> CreateModelList(IEnumerable<V> domains, int maxdepth = 1)
        {
            return domains.Select(x => CreateModel(x, maxdepth));
        }
    }
}
