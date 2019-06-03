using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;
using System;
using System.Globalization;
using System.Reflection;

namespace Library.Impl.Business
{
    public class HelperLogic<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        public static V CreateInstance(U data)
        {
            return (V)Activator.CreateInstance(typeof(V),
                           BindingFlags.CreateInstance |
                           BindingFlags.Public |
                           BindingFlags.Instance |
                           BindingFlags.OptionalParamBinding,
                           null, new object[] { data },
                           CultureInfo.CurrentCulture);
        }
    }
}
