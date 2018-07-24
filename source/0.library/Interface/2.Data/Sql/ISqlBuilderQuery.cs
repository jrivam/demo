using library.Impl.Data.Sql;
using library.Interface.Data.Query;
using System.Collections.Generic;

namespace library.Interface.Data.Sql.Builder
{
    public interface ISqlBuilderQuery : ISqlBuilder
    {
        IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)>
            GetQueryColumns
            (IQueryRepository query,
            IList<string> tablenames,
            IList<string> aliasnames,
            int maxdepth = 1, int depth = 0);

        IList<(IQueryRepository internaltable, IList<string> internalalias, IQueryRepository externaltable, IList<string> externalalias, IList<(IQueryColumn, IQueryColumn)> joins)>
            GetQueryJoins
            (IQueryRepository query,
            IList<string> prefix,
            int maxdepth = 1, int depth = 0);

        IEnumerable<((object value, WhereOperator? sign) where, SqlParameter parameter, int counter)>
            GetParameters
            ((IQueryColumn column, IList<string> aliases, IList<string> parameters) columns,
            IList<SqlParameter> parameters);

        string
            GetFrom
            (IList<(IQueryRepository internaltable, IList<string> internalalias, IQueryRepository externaltable, IList<string> externalalias, IList<(IQueryColumn internalkey, IQueryColumn externalkey)> joins)> joins,
            string tablename);

        string
            GetWhere
            (IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)> columns,
            IList<SqlParameter> parameters);

        string
            GetSelectColumns
            (IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)> columns);
    }
}
