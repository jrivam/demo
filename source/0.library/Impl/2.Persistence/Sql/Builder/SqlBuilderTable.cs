using Library.Interface.Persistence.Sql.Builder;
using Library.Interface.Persistence.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Impl.Persistence.Sql.Builder
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
                select += $"{(string.IsNullOrWhiteSpace(select) ? string.Empty : $",{Environment.NewLine}")}{c.Table.Description.Name}.{c.Description.Name} {_sqlsyntaxsign.AliasSeparatorColumnKeyword} {_sqlsyntaxsign.AliasEnclosureColumnOpen}{c.Table.Description.Reference}{_sqlsyntaxsign.AliasSeparatorColumn}{c.Description.Reference}{_sqlsyntaxsign.AliasEnclosureColumnClose}";
            }
            select = $"{(!string.IsNullOrWhiteSpace(select) ? $"{select}{Environment.NewLine}" : string.Empty)}";

            return select;
        }

        public virtual string
            GetWhere
            (IList<IColumnTable> columns, IList<SqlParameter> parameters)
        {
            var where = string.Empty;

            foreach (var p in GetParameters(columns.Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), parameters))
            {
                where += $"{(string.IsNullOrWhiteSpace(where) ? "where" : "and")} {(_sqlsyntaxsign.UpdateWhereUseAlias ? $"{p.view.Name}." : string.Empty)}{p.column.Name} {_sqlsyntaxsign.GetOperator(WhereOperator.Equals)} {p.parameter.Name}{Environment.NewLine}";
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
