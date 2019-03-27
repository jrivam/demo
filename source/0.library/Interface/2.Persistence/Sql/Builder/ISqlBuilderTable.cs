using Library.Impl.Persistence.Sql;
using Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace Library.Interface.Persistence.Sql.Builder
{
    public interface ISqlBuilderTable : ISqlBuilder
    {
        string GetSelectColumns(IList<IColumnTable> columns);
        string GetWhere(IList<IColumnTable> columns, IList<SqlParameter> parameters, bool prefixtablename = true);
        string GetInsertColumns(IList<IColumnTable> columns);
        string GetInsertValues(IList<IColumnTable> columns, IList<SqlParameter> parameters);
    }
}
