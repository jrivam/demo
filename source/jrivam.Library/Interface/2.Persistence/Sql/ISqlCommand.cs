using jrivam.Library.Impl.Persistence.Sql;
using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Interface.Persistence.Sql
{
    public interface ISqlCommand
    {
        string Text { get; set; }

        CommandType Type { get; set; }

        IList<SqlParameter> Parameters { get; set; }
    }
}
