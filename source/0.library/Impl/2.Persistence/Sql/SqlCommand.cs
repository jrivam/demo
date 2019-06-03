using Library.Interface.Persistence.Sql;
using System.Collections.Generic;
using System.Data;

namespace Library.Impl.Persistence.Sql
{
    public class SqlCommand : ISqlCommand
    {
        public string Text { get; set; } = string.Empty;
        public CommandType Type { get; set; } = CommandType.Text;
        public IList<SqlParameter> Parameters { get; set; } = new List<SqlParameter>();
    }
}
