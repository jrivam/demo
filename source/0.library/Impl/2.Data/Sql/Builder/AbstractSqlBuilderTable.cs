using library.Interface.Data.Sql;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace library.Impl.Data.Sql.Builder
{
    public abstract class AbstractSqlBuilderTable<T> : AbstractSqlBuilder, ISqlBuilderTable<T>
        where T : IEntity
    {
        public abstract (string commandtext, IList<SqlParameter> parameters) 
            Select
            (ITableRepositoryProperties<T> entitytable);

        public abstract (string commandtext, IList<SqlParameter> parameters) 
            Insert
            (ITableRepositoryProperties<T> entitytable);

        public abstract (string commandtext, IList<SqlParameter> parameters) 
            Update
            (ITableRepositoryProperties<T> entitytable);

        public abstract (string commandtext, IList<SqlParameter> parameters) 
            Delete
            (ITableRepositoryProperties<T> entitytable);

        public AbstractSqlBuilderTable(ISqlSyntaxSign syntaxsign)
            : base(syntaxsign)
        {
        }

        protected virtual string
            GetSelectColumns
            (IList<ITableColumn> columns)
        {
            var select = string.Empty;

            foreach (var c in columns)
            {
                select += $"{(string.IsNullOrWhiteSpace(select) ? "" : $",{Environment.NewLine}")}{c.TableDescription.Name}.{c.ColumnDescription.Name} {_syntaxsign.AliasSeparatorColumnKeyword} {_syntaxsign.AliasEnclosureColumnOpen}{c.TableDescription.Reference}{_syntaxsign.AliasSeparatorColumn}{c.ColumnDescription.Reference}{_syntaxsign.AliasEnclosureColumnClose}";
            }
            select = $"{(!string.IsNullOrWhiteSpace(select) ? $"{select}{Environment.NewLine}" : "")}";

            return select;
        }

        protected virtual string 
            GetWhere
            (IList<ITableColumn> columns, IList<SqlParameter> parameters)
        {
            var where = string.Empty;

            foreach (var p in GetParameters(columns, parameters))
            {
                where += $"{(string.IsNullOrWhiteSpace(where) ? "where" : "and")} {p.column.TableDescription.Name}.{p.column.ColumnDescription.Name} {_syntaxsign.GetOperator(WhereOperator.Equals)} {p.parameter.Name}{Environment.NewLine}";
            }

            return where;
        }

        protected virtual string 
            GetInsertColumns
            (IList<ITableColumn> columns)
        {
            var insert = string.Empty;

            foreach (var c in columns)
            {
                insert += $"{(string.IsNullOrWhiteSpace(insert) ? "" : $",{Environment.NewLine}")}{c.ColumnDescription.Name}";
            }
            insert = $"{(!string.IsNullOrWhiteSpace(insert) ? $"({insert}){Environment.NewLine}" : "")}";

            return insert;
        }

        protected virtual string 
            GetInsertValues
            (IList<ITableColumn> columns, IList<SqlParameter> parameters)
        {
            var values = string.Empty;

            foreach (var p in GetParameters(columns, parameters))
            {
                values += $"{(string.IsNullOrWhiteSpace(values) ? "" : $",{Environment.NewLine}")}{p.parameter.Name}";
            }
            values = $"{(!string.IsNullOrWhiteSpace(values) ? $"({values}){Environment.NewLine}" : "")}";

            return values;
        }
    }
}
