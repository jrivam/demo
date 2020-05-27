using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace jrivam.Library.Interface.Persistence.Sql.Builder
{
    public interface ISqlBuilderTable : ISqlBuilder
    {
        string GetSelectColumns(IList<IColumnTable> columns);
        string GetWhere(IList<IColumnTable> columns, IList<SqlParameter> parameters);
        string GetInsertColumns(IList<IColumnTable> columns);
        string GetInsertValues(IList<IColumnTable> columns, IList<SqlParameter> parameters);
    }
}
