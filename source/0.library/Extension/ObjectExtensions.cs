using Library.Impl.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Library.Extension
{
    public static class ObjectExtensions
    {
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

        public static T GetAttributeFromType<T>(this Type t)
            where T : Attribute
        {
            return (T)ReflectionCache.FindPropertyInfoAttributes(t).FirstOrDefault();
        }

        public static object[] GetAttributesFromTypeProperty(this Type t, string propertyName)
        {
            return ReflectionCache.FindPropertyInfoAttributes(ReflectionCache.FindTypeProperties(t).Where(x => x.Name == propertyName).FirstOrDefault());
        }

        public static IEnumerable<(PropertyInfo info, bool isprimitive, bool iscollection, bool isforeign)> GetTypeProperties(this Type t, bool isprimitive = false, bool iscollection = false, bool isforeign = false)
        {
            Func<PropertyInfo, bool> primitive = x => x.PropertyType.IsPrimitive || x.PropertyType.IsValueType || x.PropertyType == typeof(string);
            Func<PropertyInfo, bool> collection = x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>);

            var properties = ReflectionCache.FindTypeProperties(t)
                .Select(x => (info: x, isprimitive: primitive(x), iscollection: collection(x), isforeign: !(primitive(x) || collection(x))))
                .Where(x => (x.isprimitive && isprimitive) || (x.iscollection && iscollection) || (x.isforeign && isforeign));

            foreach (var property in properties)
            {
                yield return property;
            }
        }

        public static B SetProperties<A, B>(this A from, B to, bool nulls = false, bool isprimitive = false, bool iscollection = false, bool isforeign = false)
        {
            var propsto = to?.GetType().GetProperties();

            foreach (var propfrom in typeof(A).GetTypeProperties(isprimitive, iscollection, isforeign))
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
