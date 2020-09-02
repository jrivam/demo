using jrivam.Library.Interface.Persistence.Sql;
using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Impl.Persistence.Sql
{
    public class SqlCommand : ISqlCommand
    {
        public string Text { get; set; } = string.Empty;
        public CommandType Type { get; set; } = CommandType.Text;
        public int CommandTimeout { get; set; } = 30;
        public IList<ISqlParameter> Parameters { get; set; } = new List<ISqlParameter>();
    }
}
