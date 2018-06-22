using library.Interface.Data.Mapper;
using library.Interface.Data.Sql;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace library.Impl.Data.Mapper
{
    public class BaseMapperTable<T, U> : IMapperRepository<T, U> 
        where T : IEntity
        where U : ITableRepositoryProperties<T>
    {
        protected readonly ISqlSyntaxSign _syntaxsign;

        public BaseMapperTable(ISqlSyntaxSign syntaxsign)
        {
            _syntaxsign = syntaxsign;
        }

        public virtual U Read(U data, IDataReader reader, IList<string> prefixname, int maxdepth = 1, int depth = 0)
        {
            prefixname.Add(data.Description.Reference);
            var name = string.Join(_syntaxsign.AliasSeparatorColumn, prefixname);

            foreach (var column in data.Columns)
            {
                column.Value = column.DbValue = reader[$"{name}{(name == "" ? "" : _syntaxsign.AliasSeparatorColumn)}{column.ColumnDescription.Reference}"];
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
