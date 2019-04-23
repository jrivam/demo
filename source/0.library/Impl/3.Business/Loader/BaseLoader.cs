using Library.Interface.Business.Loader;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;
using System;
using System.Globalization;
using System.Reflection;

namespace Library.Impl.Domain.Loader
{
    public class BaseLoader<T, U, V> : ILoader<T, U, V> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        public BaseLoader()
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
        public virtual V Clear(V domain)
        {
            return domain;
        }

        public virtual V Load(V domain, int maxdepth = 1, int depth = 0)
        {
            return domain;
        }
        public virtual V LoadX(V domain, int maxdepth = 1, int depth = 0)
        {
            return domain;
        }
    }
}
