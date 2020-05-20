using Library.Interface.Business.Query;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Table;
using System;
using System.Globalization;
using System.Reflection;

namespace Library.Impl.Business
{
    public class HelperQueryLogic<R, S, T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where S : IQueryData<T, U>
        where R : IQueryDomain<S, T, U, V>
    {
        public static R CreateQuery(S data)
        {
            return (R)Activator.CreateInstance(typeof(R),
                           BindingFlags.CreateInstance |
                           BindingFlags.Public |
                           BindingFlags.Instance |
                           BindingFlags.OptionalParamBinding, null,
                           new object[] { data },
                           CultureInfo.CurrentCulture);
        }
    }
}
