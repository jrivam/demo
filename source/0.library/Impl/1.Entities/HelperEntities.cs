using System;
using System.Globalization;
using System.Reflection;

namespace Library.Impl.Entities
{
    public class HelperEntities<T>
    {
        public static T CreateEntity()
        {
            return (T)Activator.CreateInstance(typeof(T),
                                    BindingFlags.CreateInstance |
                                    BindingFlags.Public |
                                    BindingFlags.Instance |
                                    BindingFlags.OptionalParamBinding,
                                    null, null,
                                    CultureInfo.CurrentCulture);
        }
    }
}
