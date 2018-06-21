using library.Impl.Data;
using library.Impl.Data.Sql;
using library.Interface.Data.Query;
using System.Collections.Generic;

namespace library.Interface.Data.Sql
{
    public interface ISqlBuilderQuery : ISqlBuilder
    {
        (string commandtext, IList<SqlParameter> parameters) Select(IQueryRepositoryProperties querytable, int maxdepth = 1, int top = 0);
        (string commandtext, IList<SqlParameter> parameters) Update(IList<IEntityColumn> columns, IQueryRepositoryProperties querytable, int maxdepth = 1);
        (string commandtext, IList<SqlParameter> parameters) Delete(IQueryRepositoryProperties querytable, int maxdepth = 1);

        IEnumerable<((object value, WhereOperator? sign) where, SqlParameter parameter, int counter)> GetQueryParameters((IQueryColumn column, IList<string> aliases, IList<string> parameters) columns, IList<SqlParameter> parameters);
    }
}
