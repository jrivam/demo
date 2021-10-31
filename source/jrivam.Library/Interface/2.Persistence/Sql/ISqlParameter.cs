using System;
using System.Data;

namespace jrivam.Library.Interface.Persistence.Sql
{
    public interface ISqlParameter : IParameter
    {
        Type Type { get; set; }
        ParameterDirection Direction { get; set; }
    }
}
