using library.Impl.Data.Sql.Builder;
using library.Interface.Data.Query;
using library.Interface.Data.Sql;
using library.Interface.Data.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace library.Impl.Data.Sql.Providers.PostgreSql
{
    public class PostgreSqlBuilderQuery : AbstractSqlBuilderQuery
    {
        public PostgreSqlBuilderQuery(ISqlSyntaxSign syntaxsign)
            : base(syntaxsign)
        {
        }

        public override (string commandtext, IList<SqlParameter> parameters)
            Select
            (IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)> querycolumns,
            IList<(IQueryRepositoryProperties internaltable, IList<string> internalalias, IQueryRepositoryProperties externaltable, IList<string> externalalias, IList<(IQueryColumn, IQueryColumn)> joins)> queryjoins,
            string tablename,
            int top = 0)
        {
            string commandtext = string.Empty;
            IList<SqlParameter> parameters = new List<SqlParameter>();

            var columns = GetSelectColumns(querycolumns);

            var from = GetFrom(queryjoins, tablename);

            var where = GetWhere(querycolumns, parameters);

            commandtext = $"select{Environment.NewLine}{columns}from {from}{where}";
            commandtext += $"{(top > 0 ? $" limit {top.ToString()}" : "")}";

            return (commandtext, parameters);
        }

        public override (string commandtext, IList<SqlParameter> parameters)
            Update
            (IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)> querycolumns,
            IList<(IQueryRepositoryProperties internaltable, IList<string> internalalias, IQueryRepositoryProperties externaltable, IList<string> externalalias, IList<(IQueryColumn, IQueryColumn)> joins)> queryjoins,
            string tablename,
            IList<ITableColumn> columns)
        {
            string commandtext = string.Empty;
            IList<SqlParameter> parameters = new List<SqlParameter>();

            var set = GetUpdateSet(columns.Where(c => !c.IsIdentity && c.Value != c.DbValue).ToList(), parameters);

            var table = $"{_syntaxsign.AliasEnclosureTableOpen}{tablename}{_syntaxsign.AliasEnclosureTableClose}";

            var from = GetFrom(queryjoins, tablename);

            var where = GetWhere(querycolumns, parameters);

            commandtext = $"update{Environment.NewLine}{table}set {set}from {from}{where}";

            return (commandtext, parameters);
        }

        public override (string commandtext, IList<SqlParameter> parameters)
            Delete
            (IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)> querycolumns,
            IList<(IQueryRepositoryProperties internaltable, IList<string> internalalias, IQueryRepositoryProperties externaltable, IList<string> externalalias, IList<(IQueryColumn, IQueryColumn)> joins)> queryjoins,
            string tablename)
        {
            string commandtext = string.Empty;
            IList<SqlParameter> parameters = new List<SqlParameter>();

            var from = GetFrom(queryjoins, tablename);

            var where = GetWhere(querycolumns, parameters);

            commandtext = $"delete{Environment.NewLine}from {from}{where}";

            return (commandtext, parameters);
        }
    }
}
