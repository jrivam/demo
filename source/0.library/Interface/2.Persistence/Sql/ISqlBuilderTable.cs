using Library.Impl.Data.Sql;
using Library.Interface.Data.Table;
using System.Collections.Generic;

namespace Library.Interface.Data.Sql.Builder
{
    public interface ISqlBuilderTable : ISqlBuilder
    {
        string GetSelectColumns(IList<IColumnTable> columns);
        string GetWhere(IList<IColumnTable> columns, IList<SqlParameter> parameters, bool prefixtablename = true);
        string GetInsertColumns(IList<IColumnTable> columns);
        string GetInsertValues(IList<IColumnTable> columns, IList<SqlParameter> parameters);
    }
}
