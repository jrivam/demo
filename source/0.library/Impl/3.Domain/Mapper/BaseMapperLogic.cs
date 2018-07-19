using System;
using library.Interface.Data.Table;
using library.Interface.Domain.Mapper;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using System.Globalization;
using System.Reflection;

namespace library.Impl.Domain.Mapper
{
    public class BaseMapperLogic<T, U, V> : IMapperLogic<T, U, V> 
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where V : ITableLogic<T, U>
    {
        public BaseMapperLogic()
        {
        }

        public V CreateInstance(U data)
        {
            return (V)Activator.CreateInstance(typeof(V),
                                BindingFlags.CreateInstance |
                                BindingFlags.Public |
                                BindingFlags.Instance |
                                BindingFlags.OptionalParamBinding,
                                null, new object[] { data },
                                CultureInfo.CurrentCulture);
        }
        public virtual V Clear(V domain, int maxdepth = 1, int depth = 0)
        {
            return domain;
        }
    }
}
