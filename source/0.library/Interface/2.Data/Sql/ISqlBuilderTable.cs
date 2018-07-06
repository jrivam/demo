using library.Impl.Data.Sql;
using library.Interface.Data.Table;
using System.Collections.Generic;

namespace library.Interface.Data.Sql
{
    public interface ISqlBuilderTable : ISqlBuilder
    {
        string GetSelectColumns(IList<ITableColumn> columns);
        string GetWhere(IList<ITableColumn> columns, IList<SqlParameter> parameters);
        string GetInsertColumns(IList<ITableColumn> columns);
        string GetInsertValues(IList<ITableColumn> columns, IList<SqlParameter> parameters);
    }
}
