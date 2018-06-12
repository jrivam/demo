using System;
using System.Data;

namespace library.Impl.Data.Sql
{
    public class SqlParameter
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public object Value { get; set; }
        public ParameterDirection Direction { get; set; }
    }
}
