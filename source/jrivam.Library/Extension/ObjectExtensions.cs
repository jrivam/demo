using jrivam.Library.Impl.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace jrivam.Library.Extension
{
    public static class ObjectExtensions
    {
        public static IEnumerable<(PropertyInfo info, bool isprimitive, bool iscollection, bool isforeign)> GetPropertiesFromType(this Type t, bool isprimitive = false, bool iscollection = false, bool isforeign = false, Type[] attributetypes = null)
        {
            Func<PropertyInfo, bool> filtercustom = x => x.CustomAttributes.Any(i => (attributetypes != null && attributetypes.Contains(i.AttributeType))) || attributetypes == null;
            Func<PropertyInfo, bool> filterprimitive = x => x.PropertyType.IsPrimitive || x.PropertyType.IsValueType || x.PropertyType == typeof(string);
            Func<PropertyInfo, bool> filtercollection = x => x.PropertyType != typeof(string) && x.PropertyType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));
            Func<PropertyInfo, bool> filterforeign = x => !filterprimitive(x) && !filtercollection(x);

            var properties = ReflectionCache.FindTypeProperties(t);
            var filteredcustom = properties.Where(x => filtercustom(x));
            var proyection = filteredcustom.Select(x => (info: x, isprimitive: filterprimitive(x), iscollection: filtercollection(x), isforeign: filterforeign(x)));
            var filtered = proyection.Where(x => (x.isprimitive && isprimitive) || (x.iscollection && iscollection) || (x.isforeign && isforeign));

            foreach (var property in filtered)
            {
                yield return property;
            }
        }
        public static PropertyInfo GetPropertyFromType(this Type t, string propertyName)
        {
            return ReflectionCache.FindTypeProperties(t).Where(x => x.Name == propertyName).FirstOrDefault();
        }

        public static T GetAttributeFromType<T>(this Type t)
            where T : Attribute
        {
            return (T)ReflectionCache.FindPropertyInfoAttributes(t).FirstOrDefault();
        }
        public static object[] GetAttributesFromProperty(this Type t, string propertyName)
        {
            return ReflectionCache.FindPropertyInfoAttributes(ReflectionCache.FindTypeProperties(t).Where(x => x.Name == propertyName).FirstOrDefault());
        }

        public static T ForceType<T>(this object o)
        {
            T res = HelperEntities<T>.CreateEntity();

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
        public static B SetProperties<A, B>(this A from, B to, bool nulls = false, bool isprimitive = false, bool iscollection = false, bool isforeign = false)
        {
            var propsto = to?.GetType().GetProperties();

            foreach (var propfrom in typeof(A).GetPropertiesFromType(isprimitive, iscollection, isforeign))
            {
                var propto = propsto?.Where(x => x.Name == propfrom.info.Name).SingleOrDefault();

                var value = propfrom.info.GetValue(from);
                if (value != null || nulls)
                {
                    propto?.SetValue(to, value, null);
                }
            }

            return to;
        }
    }

    public static class ReflectionCache
    {
        private static ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>> PropertyInfoCache = new ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>>();     
        public static List<PropertyInfo> FindTypeProperties(Type type)
        {
            if (PropertyInfoCache.ContainsKey(type))
                return PropertyInfoCache[type].Values.ToList();

            var result = type.GetProperties().ToDictionary(p => p.Name, p => p);

            PropertyInfoCache.TryAdd(type, result);

            return result.Values.ToList();
        }

        private static ConcurrentDictionary<PropertyInfo, object[]> PropertyInfoCustomAttributeCache = new ConcurrentDictionary<PropertyInfo, object[]>();
        public static object[] FindPropertyInfoAttributes(PropertyInfo propertyinfo)
        {
            if (PropertyInfoCustomAttributeCache.ContainsKey(propertyinfo))
                return PropertyInfoCustomAttributeCache[propertyinfo];

            var result = propertyinfo?.GetCustomAttributes(false);

            PropertyInfoCustomAttributeCache.TryAdd(propertyinfo, result);

            return result;
        }

        private static ConcurrentDictionary<Type, object[]> TypeCustomAttributeCache = new ConcurrentDictionary<Type, object[]>();
        public static object[] FindPropertyInfoAttributes(Type propertyinfo)
        {
            if (TypeCustomAttributeCache.ContainsKey(propertyinfo))
                return TypeCustomAttributeCache[propertyinfo];

            var result = propertyinfo?.GetCustomAttributes(propertyinfo, false);

            TypeCustomAttributeCache.TryAdd(propertyinfo, result);

            return result;
        }
    }
}
