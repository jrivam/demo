using library.Impl.Data;
using library.Impl.Data.Sql;
using System.Collections.Generic;

namespace library.Interface.Data.Sql
{
    public interface ISqlBuilder
    {
        IEnumerable<(IEntityColumn column, SqlParameter parameter)> GetParameters(IList<IEntityColumn> columns, IList<SqlParameter> parameters);

        string GetOperator(WhereOperator whereoperator);
        string GetUpdateSet(IList<IEntityColumn> columns, IList<SqlParameter> parameters, bool prefixtablename = true);
    }
}
