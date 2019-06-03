using Library.Interface.Entities;
using Library.Interface.Persistence.Table;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Library.Impl.Persistence
{
    public class HelperRepository<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        public static U CreateInstance(T entity)
        {
            return (U)Activator.CreateInstance(typeof(U),
                    BindingFlags.CreateInstance |
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.OptionalParamBinding,
                    null, new object[] { entity },
                    CultureInfo.CurrentCulture);
        }

        public static IEnumerable<(string name, object value)> GetPropertiesValue(U data)
        {
            foreach (var column in data.Columns)
            {
                var name = column.Description.Reference;

                var prop = data.GetType().GetProperty(name);

                if (prop != null)
                {
                    yield return (name, prop.GetValue(data));
                }
            }
        }
    }
}
