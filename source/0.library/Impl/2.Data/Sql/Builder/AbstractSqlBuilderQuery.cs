using library.Interface.Data.Query;
using library.Interface.Data.Sql;
using library.Interface.Data.Table;
using System;
using System.Collections.Generic;
using System.Data;

namespace library.Impl.Data.Sql.Builder
{
    public abstract class AbstractSqlBuilderQuery : AbstractSqlBuilder, ISqlBuilderQuery
    {
        public abstract (string commandtext, IList<SqlParameter> parameters)
            Select
            (IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)> querycolumns,
            IList<(IQueryRepositoryProperties internaltable, string internalalias, IQueryRepositoryProperties externaltable, string externalalias, IList<(IQueryColumn, IQueryColumn)> joins)> queryjoins,
            string tablename,
            int top = 0);

        public abstract (string commandtext, IList<SqlParameter> parameters)
            Update
            (IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)> querycolumns,
            IList<(IQueryRepositoryProperties internaltable, string internalalias, IQueryRepositoryProperties externaltable, string externalalias, IList<(IQueryColumn, IQueryColumn)> joins)> queryjoins,
            string tablename,
            IList<ITableColumn> columns);

        public abstract (string commandtext, IList<SqlParameter> parameters)
            Delete
            (IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)> querycolumns,
            IList<(IQueryRepositoryProperties internaltable, string internalalias, IQueryRepositoryProperties externaltable, string externalalias, IList<(IQueryColumn, IQueryColumn)> joins)> queryjoins,
            string tablename);

        public AbstractSqlBuilderQuery(ISqlSyntaxSign syntaxsign)
            : base(syntaxsign)
        {
        }

        protected virtual IEnumerable<((object value, WhereOperator? sign) where, SqlParameter parameter, int counter)> 
            GetParameters
            ((IQueryColumn column, IList<string> aliases, IList<string> parameters) columns, 
            IList<SqlParameter> parameters)
        {
            var count = 0;
            foreach (var w in columns.column.Wheres)
            {
                count++;

                var parameter = GetParameter($"{_syntaxsign.ParameterPrefix}{string.Join(_syntaxsign.ParameterSeparator, columns.parameters)}{_syntaxsign.ParameterSeparator}{columns.column.Description.Reference}{count}", columns.column.Type, w.value, ParameterDirection.Input);

                if ((w.sign & WhereOperator.LikeBegin) == WhereOperator.LikeBegin)
                    parameter.Value = $"{_syntaxsign.WhereWildcardAny}{parameter.Value}";
                if ((w.sign & WhereOperator.LikeEnd) == WhereOperator.LikeEnd)
                    parameter.Value = $"{parameter.Value}{_syntaxsign.WhereWildcardAny}";

                if (parameters.IndexOf(parameter) < 0)
                    parameters.Add(parameter);

                yield return (w, parameter, count);
            }
        }

        protected virtual string 
            GetFrom
            (IList<(IQueryRepositoryProperties internaltable, string internalalias, IQueryRepositoryProperties externaltable, string externalalias, IList<(IQueryColumn internalkey, IQueryColumn externalkey)> joins)> joins, 
            string tablename)
        {
            var from = $"{tablename} {_syntaxsign.AliasSeparatorTableKeyword} {_syntaxsign.AliasEnclosureTableOpen}{tablename}{_syntaxsign.AliasEnclosureTableClose}{Environment.NewLine}";
            foreach (var j in joins)
            {
                from += $"left join {j.externaltable.Description.Name} {_syntaxsign.AliasSeparatorTableKeyword} {_syntaxsign.AliasEnclosureTableOpen}{j.externalalias}{_syntaxsign.AliasEnclosureTableClose}{Environment.NewLine}on ";
                var joinons = string.Empty;
                foreach (var on in j.joins)
                {
                    joinons += $"{(string.IsNullOrWhiteSpace(joinons) ? "" : "and")} {_syntaxsign.AliasEnclosureTableOpen}{j.internalalias}{_syntaxsign.AliasEnclosureTableClose}.{on.internalkey.Description.Name} = {_syntaxsign.AliasEnclosureTableOpen}{j.externalalias}{_syntaxsign.AliasEnclosureTableClose}.{on.externalkey.Description.Name}";
                }
                from += $"{joinons}{Environment.NewLine}";
            }

            return from;
        }

        protected virtual string 
            GetWhere
            (IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)> columns, 
            IList<SqlParameter> parameters)
        {
            var where = string.Empty;

            foreach (var c in columns)
            {
                if (c.column.Wheres.Count > 0)
                {
                    where += $"{(string.IsNullOrWhiteSpace(where) ? "where " : "and ")} (";

                    var columnname = $"{_syntaxsign.AliasEnclosureTableOpen}{string.Join(_syntaxsign.AliasSeparatorTable, c.tablenames)}{_syntaxsign.AliasEnclosureTableClose}.{c.column.Description.Name}";

                    foreach (var p in GetParameters(c, parameters))
                    {
                        where += $"{(p.counter > 1 ? " or " : "")}{((p.where.sign & WhereOperator.Not) == WhereOperator.Not ? "not " : "")}{columnname} {_syntaxsign.GetOperator(p.where.sign)} {p.parameter.Name}{(p.where.value == null ? $" or ({p.parameter.Name} is null and {columnname} is null)" : "")}";
                    }

                    where += $"){Environment.NewLine}";
                }
            }

            return where;
        }

        protected virtual string 
            GetSelectColumns
            (IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)> columns)
        {
            var select = string.Empty;

            var lasttable = string.Empty;
            foreach(var c in columns)
            {
                var tablename = $"{string.Join(_syntaxsign.AliasSeparatorTable, c.tablenames)}";

                select += $"{(string.IsNullOrWhiteSpace(select) ? "" : $",{(lasttable == tablename ? " " : Environment.NewLine)}")}{_syntaxsign.AliasEnclosureTableOpen}{tablename}{_syntaxsign.AliasEnclosureTableClose}.{c.column.Description.Name} {_syntaxsign.AliasSeparatorColumnKeyword} {_syntaxsign.AliasEnclosureColumnOpen}{string.Join(_syntaxsign.AliasSeparatorColumn, c.aliasnames)}{_syntaxsign.AliasSeparatorColumn}{c.column.Description.Reference}{_syntaxsign.AliasEnclosureColumnClose}";

                lasttable = tablename;
            };
            select = $"{(!string.IsNullOrWhiteSpace(select) ? $"{select}{Environment.NewLine}" : "")}";

            return select;
        }
    }
}
