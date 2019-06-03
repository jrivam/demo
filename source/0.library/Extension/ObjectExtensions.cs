using Library.Impl.Entities;
using System;
using System.Linq;

namespace Library.Extension
{
    public static class ObjectExtensions
    {
        public static T ForceType<T>(this object o)
        {
            T res;
            res = HelperEntities<T>.CreateInstance();

            Type x = o.GetType();
            Type y = res.GetType();

            foreach (var destinationProp in y.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                var sourceProp = x.GetProperty(destinationProp.Name);
                if (sourceProp != null && destinationProp.CanWrite)
                {
                    destinationProp.SetValue(res, sourceProp.GetValue(o));
                }
            }

            return res;
        }

        public static T GetAttributeFromType<T>(this Type instance)
            where T : Attribute
        {
            return (T)instance.GetCustomAttributes(typeof(T), false).FirstOrDefault();
        }
        public static T GetAttributeFromTypeProperty<T>(this Type instance, string propertyName)
            where T : Attribute
        {
            return (T)instance.GetProperty(propertyName).GetCustomAttributes(typeof(T), false).FirstOrDefault();
        }
    }
}
