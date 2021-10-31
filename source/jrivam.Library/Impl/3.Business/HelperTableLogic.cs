using Autofac;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Linq;

namespace jrivam.Library.Impl.Business
{
    public class HelperTableLogic<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        public static V CreateDomain(U data)
        {
            return AutofacConfiguration.Container.Resolve<V>(new TypedParameter(typeof(U), data));
        }

        public static IEnumerable<V> CreateDomainList(IEnumerable<U> datas)
        {
            return datas.Select(x => CreateDomain(x));
        }
    }
}
