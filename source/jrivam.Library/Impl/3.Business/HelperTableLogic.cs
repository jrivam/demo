using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace jrivam.Library.Impl.Business
{
    public class HelperTableLogic<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        public static V CreateDomain(U data)
        {
            return (V)Activator.CreateInstance(typeof(V),
                           BindingFlags.CreateInstance |
                           BindingFlags.Public |
                           BindingFlags.Instance |
                           BindingFlags.OptionalParamBinding,
                           null, new object[] { null, null, data },
                           CultureInfo.CurrentCulture);
        }

        public static IEnumerable<V> CreateDomainList(IEnumerable<U> datas)
        {
            return datas.Select(x => CreateDomain(x));
        }
    }
}
