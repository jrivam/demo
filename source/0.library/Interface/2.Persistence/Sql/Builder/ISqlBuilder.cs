using Library.Impl.Persistence;
using Library.Impl.Persistence.Sql;
using System;
using System.Collections.Generic;

namespace Library.Interface.Persistence.Sql.Builder
{
    public interface ISqlBuilder
    {
        IEnumerable<(Description view, Description column, SqlParameter parameter)>
            GetParameters
           (IList<(Description view, Description column, Type type, object value)> columns, IList<SqlParameter> parameters);

        string
            GetUpdateSet
            (IList<(Description view, Description column, Type type, object value)> columns, IList<SqlParameter> parameters);
    }
}
