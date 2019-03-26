using Library.Interface.Data.Sql;
using Library.Interface.Data.Sql.Builder;
using Library.Interface.Data.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Impl.Data.Sql.Builder
{
    public class SqlBuilderTable : AbstractSqlBuilder, ISqlBuilderTable
    {
        public SqlBuilderTable(ISqlSyntaxSign syntaxsign)
            : base(syntaxsign)
        {
        }

        public virtual string
            GetSelectColumns
            (IList<IColumnTable> columns)
        {
            var select = string.Empty;

            foreach (var c in columns)
            {
                select += $"{(string.IsNullOrWhiteSpace(select) ? string.Empty : $",{Environment.NewLine}")}{c.Table.Description.Name}.{c.Description.Name} {_syntaxsign.AliasSeparatorColumnKeyword} {_syntaxsign.AliasEnclosureColumnOpen}{c.Table.Description.Reference}{_syntaxsign.AliasSeparatorColumn}{c.Description.Reference}{_syntaxsign.AliasEnclosureColumnClose}";
            }
            select = $"{(!string.IsNullOrWhiteSpace(select) ? $"{select}{Environment.NewLine}" : string.Empty)}";

            return select;
        }

        public virtual string
            GetWhere
            (IList<IColumnTable> columns, IList<SqlParameter> parameters, bool prefixtablename = true)
        {
            var where = string.Empty;

            foreach (var p in GetParameters(columns.Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), parameters))
            {
                where += $"{(string.IsNullOrWhiteSpace(where) ? "where" : "and")} {(prefixtablename ? $"{p.view.Name}." : string.Empty)}{p.column.Name} {_syntaxsign.GetOperator(WhereOperator.Equals)} {p.parameter.Name}{Environment.NewLine}";
            }

            return where;
        }

        public virtual string
            GetInsertColumns
            (IList<IColumnTable> columns)
        {
            var insert = string.Empty;

            foreach (var c in columns)
            {
                insert += $"{(string.IsNullOrWhiteSpace(insert) ? string.Empty : $",{Environment.NewLine}")}{c.Description.Name}";
            }
            insert = $"{(!string.IsNullOrWhiteSpace(insert) ? $"({insert}){Environment.NewLine}" : string.Empty)}";

            return insert;
        }

        public virtual string
            GetInsertValues
            (IList<IColumnTable> columns, IList<SqlParameter> parameters)
        {
            var values = string.Empty;

            foreach (var p in GetParameters(columns.Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), parameters))
            {
                values += $"{(string.IsNullOrWhiteSpace(values) ? string.Empty : $",{Environment.NewLine}")}{p.parameter.Name}";
            }
            values = $"{(!string.IsNullOrWhiteSpace(values) ? $"({values}){Environment.NewLine}" : string.Empty)}";

            return values;
        }
    }
}
