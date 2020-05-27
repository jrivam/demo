using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace jrivam.Library.Impl.Persistence
{
    public class HelperTableRepository<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        public static U CreateData(T entity)
        {
            return (U)Activator.CreateInstance(typeof(U),
                    BindingFlags.CreateInstance |
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.OptionalParamBinding,
                    null, new object[] { entity },
                    CultureInfo.CurrentCulture);
        }
        public static IEnumerable<U> CreateDataList(IEnumerable<T> entities)
        {
            return entities.Select(x => CreateData(x));
        }
    }
}
