using library.Interface.Data;
using library.Interface.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace library.Impl.Data
{
    public class MapperTable<T, U> : IMapperTable<T, U> where T : IEntity
                                                        where U : IEntityTable<T>
    {
        public virtual U Read(U data, IDataReader reader, IList<string> prefixname, string aliasseparator = ".", int maxdepth = 1, int depth = 0)
        {
            prefixname.Add(data.Reference);
            var name = string.Join(aliasseparator, prefixname);

            foreach (var column in data.Columns)
            {
                column.Value = column.DbValue = reader[$"{name}{(name == "" ? "" : aliasseparator)}{column.Reference}"];
            }

            return data;
        }

        public virtual U CreateInstance(int maxdepth = 1, int depth = 0)
        {
            var instance = (U)Activator.CreateInstance(typeof(U),
                    BindingFlags.CreateInstance |
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.OptionalParamBinding, null, null, CultureInfo.CurrentCulture);

            return instance;
        }
        public virtual U Clear(U data, int maxdepth = 1, int depth = 0)
        {
            return data;
        }

        public virtual U Map(U data, int maxdepth = 1, int depth = 0)
        {
            return data;
        }
    }
}
