using Library.Extension;
using Library.Interface.Entities.Reader;
using Library.Interface.Persistence.Sql.Builder;
using System;
using System.Collections.Generic;
using System.Data;

namespace Library.Impl.Entities.Reader
{
    public class BaseReader<T> : IReader<T>
    {
        protected readonly ISqlSyntaxSign _sqlsyntaxsign;

        public BaseReader(ISqlSyntaxSign sqlsyntaxsign)
        {
            _sqlsyntaxsign = sqlsyntaxsign;
        }

        public virtual T Clear(T entity)
        {
            foreach (var property in entity.GetProperties(isprimitive: true))
            {
                property?.SetValue(entity, null);
            }

            return entity;
        }

        public virtual T Read(T entity, IDataReader reader, IList<string> prefixname, int maxdepth = 1, int depth = 0)
        {
            prefixname.Add(entity?.GetType().Name);

            var prefix = string.Join(_sqlsyntaxsign.AliasSeparatorColumn, prefixname);
            prefix += (prefix == string.Empty ? prefix : _sqlsyntaxsign.AliasSeparatorColumn);

            foreach (var property in entity.GetProperties(isprimitive: true))
            {
                var value = reader[$"{prefix}{property.Name}"];

                Type t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                object converted = (value == null) ? null : Convert.ChangeType(value, t);

                property.SetValue(entity, converted);
            }

            return entity;
        }
        public virtual T ReadX(T entity, IDataReader reader, IList<string> prefixname, int maxdepth = 1, int depth = 0)
        {
            return entity;
        }
    }
}
