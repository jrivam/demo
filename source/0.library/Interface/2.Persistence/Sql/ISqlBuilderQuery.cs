using Library.Impl.Data.Sql;
using Library.Interface.Data.Query;
using System.Collections.Generic;

namespace Library.Interface.Data.Sql.Builder
{
    public interface ISqlBuilderQuery : ISqlBuilder
    {
        IList<(IColumnQuery column, IList<string> tablenames, IList<string> aliasnames)>
            GetQueryColumns
            (IBuilderQueryData query,
            IList<string> tablenames,
            IList<string> aliasnames,
            int maxdepth = 1, int depth = 0);

        IList<(IBuilderQueryData internaltable, IList<string> internalalias, IBuilderQueryData externaltable, IList<string> externalalias, IList<(IColumnQuery, IColumnQuery)> joins)>
            GetQueryJoins
            (IBuilderQueryData query,
            IList<string> prefix,
            int maxdepth = 1, int depth = 0);

        IEnumerable<((object value, WhereOperator? sign) where, SqlParameter parameter, int counter)>
            GetParameters
            ((IColumnQuery column, IList<string> aliases, IList<string> parameters) columns,
            IList<SqlParameter> parameters);

        string
            GetFrom
            (IList<(IBuilderQueryData internaltable, IList<string> internalalias, IBuilderQueryData externaltable, IList<string> externalalias, IList<(IColumnQuery internalkey, IColumnQuery externalkey)> joins)> joins,
            string tablename);

        string
            GetWhere
            (IList<(IColumnQuery column, IList<string> tablenames, IList<string> aliasnames)> columns,
            IList<SqlParameter> parameters);

        string
            GetSelectColumns
            (IList<(IColumnQuery column, IList<string> tablenames, IList<string> aliasnames)> columns);
    }
}
