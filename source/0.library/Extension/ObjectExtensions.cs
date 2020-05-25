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
            return (T)t.GetCustomAttributes(typeof(T), false).FirstOrDefault();
        }

        public static object[] GetAttributesFromTypeProperty(this Type t, string propertyName)
        {
            //return t.GetProperty(propertyName).GetCustomAttributes(false);
            return ReflectionCache.FindClassProperties(t).Where(x => x.Name == propertyName).FirstOrDefault()?.GetCustomAttributes(false);
        }

        public static IEnumerable<(PropertyInfo info, bool isprimitive, bool iscollection, bool isforeign)> GetTypeProperties(this Type t, bool isprimitive = false, bool iscollection = false, bool isforeign = false)
        {
            Func<PropertyInfo, bool> primitive = x => x.PropertyType.IsPrimitive || x.PropertyType.IsValueType || x.PropertyType == typeof(string);
            Func<PropertyInfo, bool> collection = x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>);

            var properties = ReflectionCache.FindClassProperties(t)
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
        private static ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>> reflectionPropertyCache = new ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>>();
        public static List<PropertyInfo> FindClassProperties(Type objectType)
        {
            if (reflectionPropertyCache.ContainsKey(objectType))
                return reflectionPropertyCache[objectType].Values.ToList();

            var result = objectType.GetProperties().ToDictionary(p => p.Name, p => p);

            reflectionPropertyCache.TryAdd(objectType, result);

            return result.Values.ToList();
        }
    }
}
