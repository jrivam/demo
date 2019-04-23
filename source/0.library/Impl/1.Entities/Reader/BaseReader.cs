using Library.Interface.Entities.Reader;
using Library.Interface.Persistence.Sql.Builder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace Library.Impl.Entities.Reader
{
    public class BaseReader<T> : IReader<T>
    {
        protected readonly ISqlSyntaxSign _sqlsyntaxsign;

        public BaseReader(ISqlSyntaxSign sqlsyntaxsign)
        {
            _sqlsyntaxsign = sqlsyntaxsign;
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

        public virtual T Clear(T entity)
        {
            var props = entity?.GetType().GetProperties();

            foreach (var prop in props)
            {
                prop?.SetValue(entity, null, null);
            }

            return entity;
        }

        public virtual T Read(T entity, IDataReader reader, IList<string> prefixname, int maxdepth = 1, int depth = 0)
        {
            prefixname.Add(entity?.GetType().Name);

            var prefix = string.Join(_sqlsyntaxsign.AliasSeparatorColumn, prefixname);
            prefix += (prefix == string.Empty ? prefix : _sqlsyntaxsign.AliasSeparatorColumn);

            var props = entity?.GetType().GetProperties();

            foreach (var prop in props)
            {
                if (prop.PropertyType.IsPrimitive || prop.PropertyType.IsValueType || (prop.PropertyType == typeof(string)))
                {
                    var value = reader[$"{prefix}{prop.Name}"];

                    Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                    object converted = (value == null) ? null : Convert.ChangeType(value, t);

                    prop?.SetValue(entity, converted, null);

                }
            }

            return entity;
        }
        public virtual T ReadX(T entity, IDataReader reader, IList<string> prefixname, int maxdepth = 1, int depth = 0)
        {
            return entity;
        }
    }
}
