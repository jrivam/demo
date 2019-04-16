using Library.Interface.Entities.Reader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace Library.Impl.Entities.Reader
{
    public class BaseReader<T> : IReader<T>
    {
        public BaseReader()
        {
        }

        public virtual T CreateInstance()
        {
            var instance = (T)Activator.CreateInstance(typeof(T),
                    BindingFlags.CreateInstance |
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.OptionalParamBinding,
                    null, null, 
                    CultureInfo.CurrentCulture);

            return instance;
        }

        public virtual T Clear(T entity, int maxdepth = 1, int depth = 0)
        {
            return entity;
        }

        public virtual T Read(T entity, IDataReader reader, IList<string> prefixname, string columnseparator, int maxdepth = 1, int depth = 0)
        {
            return entity;
        }
    }
}
