using jrivam.Library.Extension;
using jrivam.Library.Interface.Entities.Reader;
using jrivam.Library.Interface.Persistence.Sql.Builder;
using System;
using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Impl.Entities.Reader
{
    public class EntityReader : IEntityReader
    {
        protected readonly ISqlSyntaxSign _sqlsyntaxsign;

        public EntityReader(ISqlSyntaxSign sqlsyntaxsign)
        {
            _sqlsyntaxsign = sqlsyntaxsign;
        }

        public virtual void Clear<T>(T entity)
        {
            foreach (var property in typeof(T).GetPropertiesFromType(isprimitive: true, isforeign: true))
            {
                property.info?.SetValue(entity, null);
            }
        }

        public virtual void Read<T>(T entity, IDataReader datareader, IList<string> prefixname, int maxdepth = 1, int depth = 0)
        {
            prefixname.Add(entity?.GetType().Name);

            var prefix = string.Join(_sqlsyntaxsign.AliasSeparatorColumn, prefixname);
            prefix += (prefix == string.Empty ? prefix : _sqlsyntaxsign.AliasSeparatorColumn);

            foreach (var property in typeof(T).GetPropertiesFromType(isprimitive: true, isforeign: true))
            {
                if (property.isprimitive)
                {
                    var value = datareader[$"{prefix}{property.info.Name}"];

                    property.info.SetValue(entity, value ?? Convert.ChangeType(value, Nullable.GetUnderlyingType(property.info.PropertyType) ?? property.info.PropertyType));
                }

                if (property.isforeign)
                {
                    depth++;
                    if (depth < maxdepth || maxdepth == 0)
                    {
                        property.info.SetValue(entity, Activator.CreateInstance(property.info.PropertyType));
                        var foreign = property.info.GetValue(entity);

                        this.GetType()
                            .GetMethod(nameof(Read))
                            .MakeGenericMethod(property.info.PropertyType)
                            .Invoke(this, new object[] { foreign, datareader, prefixname, maxdepth, depth });
                    }
                }
            }
        }
    }
}
