using library.Impl.Data.Sql;
using library.Interface.Data.Table;
using System.Collections.Generic;

namespace library.Interface.Data.Sql.Builder
{
    public interface ISqlBuilderTable : ISqlBuilder
    {
        string GetSelectColumns(IList<(ITableRepository table, ITableColumn column)> tablecolumns);
        string GetWhere(IList<(ITableRepository table, ITableColumn column)> tablecolumns, IList<SqlParameter> parameters, bool prefixtablename = true);
        string GetInsertColumns(IList<(ITableRepository table, ITableColumn column)> tablecolumns);
        string GetInsertValues(IList<(ITableRepository table, ITableColumn column)> tablecolumns, IList<SqlParameter> parameters);
    }
}
