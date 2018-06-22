using library.Impl.Data.Sql;
using library.Interface.Data.Table;
using System.Collections.Generic;

namespace library.Interface.Data.Sql
{
    public interface ISqlBuilder
    {
        IEnumerable<(ITableColumn column, SqlParameter parameter)> GetParameters(IList<ITableColumn> columns, IList<SqlParameter> parameters);

        string GetUpdateSet(IList<ITableColumn> columns, IList<SqlParameter> parameters, bool prefixtablename = true);
    }
}
