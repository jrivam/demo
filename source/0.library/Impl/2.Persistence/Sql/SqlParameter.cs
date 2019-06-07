using Library.Interface.Persistence.Sql;
using System;
using System.Data;

namespace Library.Impl.Persistence.Sql
{
    public class SqlParameter : Parameter, ISqlParameter
    {
        public Type Type { get; set; }
        public ParameterDirection Direction { get; set; }
    }
}
