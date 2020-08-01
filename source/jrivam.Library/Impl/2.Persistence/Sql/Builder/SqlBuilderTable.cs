using jrivam.Library.Interface.Persistence.Sql;
using jrivam.Library.Interface.Persistence.Sql.Builder;
using jrivam.Library.Interface.Persistence.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace jrivam.Library.Impl.Persistence.Sql.Builder
{
    public class SqlBuilderTable : AbstractSqlBuilder, ISqlBuilderTable
    {
        public SqlBuilderTable(ISqlSyntaxSign sqlsyntaxsign)
            : base(sqlsyntaxsign)
        {
        }

        public virtual string GetSelectColumns
            (IList<IColumnTable> columns)
        {
            var select = string.Empty;

            foreach (var c in columns)
            {
                select += $"{(string.IsNullOrWhiteSpace(select) ? string.Empty : $",{Environment.NewLine}")}{c.Table.Description.DbName}.{c.Description.DbName} {_sqlsyntaxsign.AliasSeparatorColumnKeyword} {_sqlsyntaxsign.AliasEnclosureColumnOpen}{c.Table.Description.Name}{_sqlsyntaxsign.AliasSeparatorColumn}{c.Description.Name}{_sqlsyntaxsign.AliasEnclosureColumnClose}";
            }
            select = $"{(!string.IsNullOrWhiteSpace(select) ? $"{select}{Environment.NewLine}" : string.Empty)}";

            return select;
        }

        public virtual string GetWhere
            (IList<IColumnTable> columns, IList<ISqlParameter> parameters)
        {
            var where = string.Empty;

            foreach (var p in GetParameters(columns.ToList(), parameters))
            {
                where += $"{(string.IsNullOrWhiteSpace(where) ? "where" : "and")} {(_sqlsyntaxsign.UpdateWhereUseAlias ? $"{p.table.DbName}." : string.Empty)}{p.column.DbName} {_sqlsyntaxsign.GetOperator(WhereOperator.Equals)} {p.parameter.Name}{Environment.NewLine}";
            }

            return where;
        }

        public virtual string GetInsertColumns
            (IList<IColumnTable> columns)
        {
            var insert = string.Empty;

            foreach (var c in columns)
            {
                insert += $"{(string.IsNullOrWhiteSpace(insert) ? string.Empty : $",{Environment.NewLine}")}{c.Description.DbName}";
            }
            insert = $"{(!string.IsNullOrWhiteSpace(insert) ? $"({insert}){Environment.NewLine}" : string.Empty)}";

            return insert;
        }

        public virtual string GetInsertValues
            (IList<IColumnTable> columns, IList<ISqlParameter> parameters)
        {
            var values = string.Empty;

            foreach (var p in GetParameters(columns.ToList(), parameters))
            {
                values += $"{(string.IsNullOrWhiteSpace(values) ? string.Empty : $",{Environment.NewLine}")}{p.parameter.Name}";
            }
            values = $"{(!string.IsNullOrWhiteSpace(values) ? $"({values}){Environment.NewLine}" : string.Empty)}";

            return values;
        }
    }
}
