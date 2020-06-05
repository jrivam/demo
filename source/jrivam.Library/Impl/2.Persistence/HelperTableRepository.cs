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
                    null, new object[] { null, null, entity },
                    CultureInfo.CurrentCulture);
        }
        public static IEnumerable<U> CreateDataList(IEnumerable<T> entities)
        {
            return entities.Select(x => CreateData(x));
        }

        //public static IQueryData<T,U> CreateQuery()
        //{
        //    //var column = (IColumnQuery)Activator.CreateInstance(typeof(ColumnQuery<>).MakeGenericType(property.info.PropertyType),
        //    //                    new object[] { this, property.info.Name, dbname });

        //    //this.GetType()
        //    //        .GetMethod(nameof(Read))
        //    //        .MakeGenericMethod(property.info.PropertyType)
        //    //        .Invoke(this, new object[] { foreign, datareader, prefixname, maxdepth, depth });

        //    //this.GetType()
        //    //        .GetMethod(nameof(Map))
        //    //        .MakeGenericMethod(property.info.PropertyType.BaseType.GetGenericArguments())
        //    //        .Invoke(this, new object[] { foreign, maxdepth, depth });

        //    //Type classType = typeof(IQueryData<T, U>).MakeGenericType();

        //    //return (IQueryData<T, U>)Activator.CreateInstance(typeof(U),
        //    //        BindingFlags.CreateInstance |
        //    //        BindingFlags.Public |
        //    //        BindingFlags.Instance |
        //    //        BindingFlags.OptionalParamBinding,
        //    //        null, new object[] { entity },
        //    //        CultureInfo.CurrentCulture);
        //}
    }
}
