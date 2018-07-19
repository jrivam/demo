using library.Impl.Data.Sql;
using library.Interface.Data.Table;
using System.Collections.Generic;

namespace library.Interface.Data.Sql
{
    public interface ISqlBuilder
    {
        IEnumerable<((ITableRepository table, ITableColumn column) tablecolumn, SqlParameter parameter)>
            GetParameters
            (IList<(ITableRepository table, ITableColumn column)> tablecolumns, IList<SqlParameter> parameters);

        string
            GetUpdateSet
            (IList<(ITableRepository table, ITableColumn column)> tablecolumns, IList<SqlParameter> parameters, bool prefixtablename = true);
    }
}
