using library.Impl.Data.Sql.Builder;
using library.Interface.Data.Query;
using library.Interface.Data.Sql;
using library.Interface.Data.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace library.Impl.Data.Sql.Providers.SqlServer
{
    public class SqlServerBuilderQuery : AbstractSqlBuilderQuery
    {
        public SqlServerBuilderQuery(ISqlSyntaxSign syntaxsign)
            : base(syntaxsign)
        {

        }

        public override (string commandtext, IList<SqlParameter> parameters) Select(IQueryRepositoryProperties querytable, int maxdepth = 1, int top = 0)
        {
            string commandtext = string.Empty;
            IList<SqlParameter> parameters = new List<SqlParameter>();

            var querycolumns = GetQueryColumns(querytable, null, null, maxdepth, 0);
            var queryjoins = GetQueryJoins(querytable, querytable.Description.Name, maxdepth, 0);

            var columns = GetQuerySelectColumns(querycolumns);

            var from = GetQueryFrom(queryjoins, querytable.Description.Name);

            var where = GetQueryWhere(querycolumns, parameters);

            commandtext = $"select{Environment.NewLine}{columns}from {from}{where}";
            commandtext = commandtext.Replace("select", $"select{(top > 0 ? $" top {top.ToString()}" : "")}");

            return (commandtext, parameters);
        }

        public override (string commandtext, IList<SqlParameter> parameters) Update(IList<ITableColumn> columns, IQueryRepositoryProperties querytable, int maxdepth = 1)
        {
            string commandtext = string.Empty;
            IList<SqlParameter> parameters = new List<SqlParameter>();

            var querycolumns = GetQueryColumns(querytable, null, null, maxdepth, 0);
            var queryjoins = GetQueryJoins(querytable, querytable.Description.Name, maxdepth, 0);

            var set = GetUpdateSet(columns.Where(c => !c.IsIdentity && c.Value != c.DbValue).ToList(), parameters);

            var table = $"{_syntaxsign.AliasEnclosureTableOpen}{querytable.Description.Name}{_syntaxsign.AliasEnclosureTableClose}";

            var from = GetQueryFrom(queryjoins, querytable.Description.Name);

            var where = GetQueryWhere(querycolumns, parameters);

            commandtext = $"update{Environment.NewLine}{table}set {set}from {from}{where}";

            return (commandtext, parameters);
        }
        public override (string commandtext, IList<SqlParameter> parameters) Delete(IQueryRepositoryProperties querytable, int maxdepth = 1)
        {
            string commandtext = string.Empty;
            IList<SqlParameter> parameters = new List<SqlParameter>();

            var querycolumns = GetQueryColumns(querytable, null, null, maxdepth, 0);
            var queryjoins = GetQueryJoins(querytable, querytable.Description.Name, maxdepth, 0);

            var table = $"{_syntaxsign.AliasEnclosureTableOpen}{querytable.Description.Name}{_syntaxsign.AliasEnclosureTableClose}{Environment.NewLine}";

            var from = GetQueryFrom(queryjoins, querytable.Description.Name);

            var where = GetQueryWhere(querycolumns, parameters);

            commandtext = $"delete{Environment.NewLine}{table}from {from}{where}";

            return (commandtext, parameters);
        }
    }
}
