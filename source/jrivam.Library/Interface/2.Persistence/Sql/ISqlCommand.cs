using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Interface.Persistence.Sql
{
    public interface ISqlCommand
    {
        string Text { get; set; }

        CommandType Type { get; set; }

        int CommandTimeout { get; set; }

        IList<ISqlParameter> Parameters { get; set; }
    }
}
