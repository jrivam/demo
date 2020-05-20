using Library.Interface.Entities;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Table;
using System;
using System.Globalization;
using System.Reflection;

namespace Library.Impl.Persistence
{
    public class HelperQueryRepository<S, T, U>
        where T : IEntity
        where U : ITableData<T, U>
        where S : IQueryData<T, U>
    {
        public static S CreateQuery()
        {
            return (S)Activator.CreateInstance(typeof(S),
                           BindingFlags.CreateInstance |
                           BindingFlags.Public |
                           BindingFlags.Instance |
                           BindingFlags.OptionalParamBinding, null,
                           new object[] { },
                           CultureInfo.CurrentCulture);
        }
    }
}
