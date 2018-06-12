using library.Interface.Data;
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
        public abstract (string commandtext, IList<SqlParameter> parameters) Select(IEntityRepositoryProperties<T> entitytable);

        public abstract (string commandtext, IList<SqlParameter> parameters) Insert(IEntityRepositoryProperties<T> entitytable);
        public abstract (string commandtext, IList<SqlParameter> parameters) Update(IEntityRepositoryProperties<T> entitytable);
        public abstract (string commandtext, IList<SqlParameter> parameters) Delete(IEntityRepositoryProperties<T> entitytable);

        public AbstractSqlBuilderTable(ISqlSyntaxSign syntaxsign)
            : base(syntaxsign)
        {
        }

        public virtual IEnumerable<(IEntityColumn column, SqlParameter parameter)> GetEntityParameters(IList<IEntityColumn> columns, IList<SqlParameter> parameters)
        {
            foreach (var c in columns)
            {
                var parameter = GetParameter($"{_syntaxsign.ParameterPrefix}{c.TableDescription.Reference}{_syntaxsign.ParameterSeparator}{c.ColumnDescription.Reference}", c.Type, c.Value, ParameterDirection.Input);
                if (parameters.IndexOf(parameter) < 0)
                    parameters.Add(parameter);

                yield return (c, parameter);
            }
        }

        protected virtual string GetEntityWhere(IList<IEntityColumn> columns, IList<SqlParameter> parameters)
        {
            var where = string.Empty;

            foreach (var p in GetEntityParameters(columns, parameters))
            {
                where += $"{(string.IsNullOrWhiteSpace(where) ? "where" : "and")} {p.column.TableDescription.Name}.{p.column.ColumnDescription.Name} {GetOperator(WhereOperator.Equals)} {p.parameter.Name}{Environment.NewLine}";
            }

            return where;
        }
        protected virtual string GetEntitySelectColumns(IList<IEntityColumn> columns)
        {
            var select = string.Empty;

            foreach (var c in columns)
            {
                select += $"{(string.IsNullOrWhiteSpace(select) ? "" : $",{Environment.NewLine}")}{c.TableDescription.Name}.{c.ColumnDescription.Name} {_syntaxsign.AliasSeparatorColumnKeyword} {_syntaxsign.AliasEnclosureColumnOpen}{c.TableDescription.Reference}{_syntaxsign.AliasSeparatorColumn}{c.ColumnDescription.Reference}{_syntaxsign.AliasEnclosureColumnClose}";
            }
            select = $"{(!string.IsNullOrWhiteSpace(select) ? $"{select}{Environment.NewLine}" : "")}";

            return select;
        }
        protected virtual string GetEntityInsertColumns(IList<IEntityColumn> columns)
        {
            var insert = string.Empty;

            foreach (var c in columns)
            {
                insert += $"{(string.IsNullOrWhiteSpace(insert) ? "" : $",{Environment.NewLine}")}{c.ColumnDescription.Name}";
            }
            insert = $"{(!string.IsNullOrWhiteSpace(insert) ? $"({insert}){Environment.NewLine}" : "")}";

            return insert;
        }
        protected virtual string GetEntityInsertValues(IList<IEntityColumn> columns, IList<SqlParameter> parameters)
        {
            var values = string.Empty;

            foreach (var p in GetEntityParameters(columns, parameters))
            {
                values += $"{(string.IsNullOrWhiteSpace(values) ? "" : $",{Environment.NewLine}")}{p.parameter.Name}";
            }
            values = $"{(!string.IsNullOrWhiteSpace(values) ? $"({values}){Environment.NewLine}" : "")}";

            return values;
        }
    }
}
