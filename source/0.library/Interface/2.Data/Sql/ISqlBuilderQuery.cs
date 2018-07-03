using library.Impl.Data.Sql;
using library.Interface.Data.Query;
using library.Interface.Data.Table;
using System.Collections.Generic;

namespace library.Interface.Data.Sql
{
    public interface ISqlBuilderQuery : ISqlBuilder
    {
        (string commandtext, IList<SqlParameter> parameters) 
            Select
            (IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)> querycolumns, 
            IList<(IQueryRepositoryProperties internaltable, string internalalias, IQueryRepositoryProperties externaltable, string externalalias, IList<(IQueryColumn, IQueryColumn)> joins)> queryjoins, 
            string tablename,
            int top = 0);

        (string commandtext, IList<SqlParameter> parameters)
            Update
            (IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)> querycolumns,
            IList<(IQueryRepositoryProperties internaltable, string internalalias, IQueryRepositoryProperties externaltable, string externalalias, IList<(IQueryColumn, IQueryColumn)> joins)> queryjoins,
            string tablename,
            IList<ITableColumn> columns);

        (string commandtext, IList<SqlParameter> parameters)
            Delete
            (IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)> querycolumns,
            IList<(IQueryRepositoryProperties internaltable, string internalalias, IQueryRepositoryProperties externaltable, string externalalias, IList<(IQueryColumn, IQueryColumn)> joins)> queryjoins,
            string tablename);

        IEnumerable<((object value, WhereOperator? sign) where, SqlParameter parameter, int counter)>
            GetQueryParameters
            ((IQueryColumn column, IList<string> aliases, IList<string> parameters) columns,
            IList<SqlParameter> parameters);
    }
}
