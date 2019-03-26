using System;
using Library.Interface.Data.Table;
using Library.Interface.Domain.Mapper;
using Library.Interface.Domain.Table;
using Library.Interface.Entities;
using System.Globalization;
using System.Reflection;

namespace Library.Impl.Domain.Mapper
{
    public class BaseMapperLogic<T, U, V> : IMapperLogic<T, U, V> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
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
        public virtual V Load(V domain, int maxdepth = 1, int depth = 0)
        {
            return domain;
        }

        public virtual V Extra(V domain, int maxdepth = 1, int depth = 0)
        {
            return domain;
        }
    }
}
