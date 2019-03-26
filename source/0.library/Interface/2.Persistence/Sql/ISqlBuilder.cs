using Library.Impl.Data;
using Library.Impl.Data.Sql;
using System;
using System.Collections.Generic;

namespace Library.Interface.Data.Sql
{
    public interface ISqlBuilder
    {
        IEnumerable<(Description view, Description column, SqlParameter parameter)>
            GetParameters
           (IList<(Description view, Description column, Type type, object value)> columns, IList<SqlParameter> parameters);

        string
            GetUpdateSet
            (IList<(Description view, Description column, Type type, object value)> columns, IList<SqlParameter> parameters, bool prefixtablename = true);
    }
}
