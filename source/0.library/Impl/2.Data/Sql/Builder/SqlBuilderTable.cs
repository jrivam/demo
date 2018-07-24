using library.Interface.Data.Sql;
using library.Interface.Data.Sql.Builder;
using library.Interface.Data.Table;
using System;
using System.Collections.Generic;

namespace library.Impl.Data.Sql.Builder
{
    public class SqlBuilderTable : AbstractSqlBuilder, ISqlBuilderTable
    {
        public SqlBuilderTable(ISqlSyntaxSign syntaxsign)
            : base(syntaxsign)
        {
        }

        public virtual string
            GetSelectColumns
            (IList<(ITableRepository table, ITableColumn column)> tablecolumns)
        {
            var select = string.Empty;

            foreach (var c in tablecolumns)
            {
                select += $"{(string.IsNullOrWhiteSpace(select) ? string.Empty : $",{Environment.NewLine}")}{c.table.Description.Name}.{c.column.Description.Name} {_syntaxsign.AliasSeparatorColumnKeyword} {_syntaxsign.AliasEnclosureColumnOpen}{c.table.Description.Reference}{_syntaxsign.AliasSeparatorColumn}{c.column.Description.Reference}{_syntaxsign.AliasEnclosureColumnClose}";
            }
            select = $"{(!string.IsNullOrWhiteSpace(select) ? $"{select}{Environment.NewLine}" : string.Empty)}";

            return select;
        }

        public virtual string
            GetWhere
            (IList<(ITableRepository table, ITableColumn column)> tablecolumns, IList<SqlParameter> parameters)
        {
            var where = string.Empty;

            foreach (var p in GetParameters(tablecolumns, parameters))
            {
                where += $"{(string.IsNullOrWhiteSpace(where) ? "where" : "and")} {p.tablecolumn.table.Description.Name}.{p.tablecolumn.column.Description.Name} {_syntaxsign.GetOperator(WhereOperator.Equals)} {p.parameter.Name}{Environment.NewLine}";
            }

            return where;
        }

        public virtual string
            GetInsertColumns
            (IList<(ITableRepository table, ITableColumn column)> tablecolumns)
        {
            var insert = string.Empty;

            foreach (var c in tablecolumns)
            {
                insert += $"{(string.IsNullOrWhiteSpace(insert) ? string.Empty : $",{Environment.NewLine}")}{c.column.Description.Name}";
            }
            insert = $"{(!string.IsNullOrWhiteSpace(insert) ? $"({insert}){Environment.NewLine}" : string.Empty)}";

            return insert;
        }

        public virtual string
            GetInsertValues
            (IList<(ITableRepository table, ITableColumn column)> tablecolumns, IList<SqlParameter> parameters)
        {
            var values = string.Empty;

            foreach (var p in GetParameters(tablecolumns, parameters))
            {
                values += $"{(string.IsNullOrWhiteSpace(values) ? string.Empty : $",{Environment.NewLine}")}{p.parameter.Name}";
            }
            values = $"{(!string.IsNullOrWhiteSpace(values) ? $"({values}){Environment.NewLine}" : string.Empty)}";

            return values;
        }
    }
}
