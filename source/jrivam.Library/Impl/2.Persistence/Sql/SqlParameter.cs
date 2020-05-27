using jrivam.Library.Interface.Persistence.Sql;
using System;
using System.Data;

namespace jrivam.Library.Impl.Persistence.Sql
{
    public class SqlParameter : Parameter, ISqlParameter
    {
        public Type Type { get; set; }
        public ParameterDirection Direction { get; set; }
    }
}
