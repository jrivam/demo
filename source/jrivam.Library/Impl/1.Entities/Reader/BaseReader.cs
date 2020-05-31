﻿using jrivam.Library.Extension;
using jrivam.Library.Interface.Entities.Reader;
using jrivam.Library.Interface.Persistence.Sql.Builder;
using System;
using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Impl.Entities.Reader
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
            foreach (var property in typeof(T).GetPropertiesFromType(isprimitive: true))
            {
                property.info?.SetValue(entity, null);
            }

            return entity;
        }

        public virtual T Read(T entity, IDataReader reader, IList<string> prefixname, int maxdepth = 1, int depth = 0)
        {
            prefixname.Add(entity?.GetType().Name);

            var prefix = string.Join(_sqlsyntaxsign.AliasSeparatorColumn, prefixname);
            prefix += (prefix == string.Empty ? prefix : _sqlsyntaxsign.AliasSeparatorColumn);

            foreach (var property in typeof(T).GetPropertiesFromType(isprimitive: true))
            {
                var value = reader[$"{prefix}{property.info.Name}"];

                property.info.SetValue(entity, value ?? Convert.ChangeType(value, Nullable.GetUnderlyingType(property.info.PropertyType) ?? property.info.PropertyType));
            }

            return entity;
        }
    }
}