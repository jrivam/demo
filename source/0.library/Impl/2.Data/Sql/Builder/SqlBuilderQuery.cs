using library.Interface.Data.Query;
using library.Interface.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;

namespace library.Impl.Data.Sql.Builder
{
    public class SqlBuilderQuery : AbstractSqlBuilder, ISqlBuilderQuery
    {
        public SqlBuilderQuery(ISqlSyntaxSign syntaxsign)
            : base(syntaxsign)
        {
        }

        public virtual IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)>
            GetQueryColumns
            (IQueryRepositoryProperties query,
            IList<string> tablenames,
            IList<string> aliasnames,
            int maxdepth = 1, int depth = 0)
        {
            var columns = new List<(IQueryColumn, IList<string>, IList<string>)>();

            if (tablenames == null)
                tablenames = new List<string>();
            tablenames.Add(query.Description.Name);

            if (aliasnames == null)
                aliasnames = new List<string>();
            aliasnames.Add(query.Description.Reference);

            foreach (var c in query.Columns)
            {
                columns.Add((c, new List<string>(tablenames), new List<string>(aliasnames)));
            }

            depth++;
            foreach (var j in query.GetJoins(maxdepth, depth))
            {
                columns.AddRange(GetQueryColumns(j.externalkey.Query, tablenames, aliasnames, maxdepth, depth));
            }

            return columns;
        }

        public virtual IList<(IQueryRepositoryProperties internaltable, IList<string> internalalias, IQueryRepositoryProperties externaltable, IList<string> externalalias, IList<(IQueryColumn, IQueryColumn)> joins)>
            GetQueryJoins
            (IQueryRepositoryProperties query,
            IList<string> prefix,
            int maxdepth = 1, int depth = 0)
        {
            var joins = new List<(IQueryRepositoryProperties, IList<string>, IQueryRepositoryProperties, IList<string>, IList<(IQueryColumn, IQueryColumn)>)>();

            depth++;
            foreach (var j in query.GetJoins(maxdepth, depth))
            {
                var tablename = new List<string>(prefix);
                tablename.Add(j.externalkey.Query.Description.Name);

                joins.Add((query, prefix, j.externalkey.Query, tablename, new List<(IQueryColumn, IQueryColumn)>() { (j.internalkey, j.externalkey) }));

                joins.AddRange(GetQueryJoins(j.externalkey.Query, tablename, maxdepth, depth));
            }

            return joins;
        }    

        public virtual IEnumerable<((object value, WhereOperator? sign) where, SqlParameter parameter, int counter)> 
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

        public virtual string 
            GetFrom
            (IList<(IQueryRepositoryProperties internaltable, IList<string> internalalias, IQueryRepositoryProperties externaltable, IList<string> externalalias, IList<(IQueryColumn internalkey, IQueryColumn externalkey)> joins)> joins, 
            string tablename)
        {
            var from = $"{tablename} {_syntaxsign.AliasSeparatorTableKeyword} {_syntaxsign.AliasEnclosureTableOpen}{tablename}{_syntaxsign.AliasEnclosureTableClose}{Environment.NewLine}";
            foreach (var j in joins)
            {
                var internalalias = string.Join(_syntaxsign.AliasSeparatorTable, j.internalalias);
                var externalalias = string.Join(_syntaxsign.AliasSeparatorTable, j.externalalias);
                    
                from += $"left join {j.externaltable.Description.Name} {_syntaxsign.AliasSeparatorTableKeyword} {_syntaxsign.AliasEnclosureTableOpen}{externalalias}{_syntaxsign.AliasEnclosureTableClose}{Environment.NewLine}on ";
                var joinons = string.Empty;
                foreach (var on in j.joins)
                {
                    joinons += $"{(string.IsNullOrWhiteSpace(joinons) ? "" : "and")} {_syntaxsign.AliasEnclosureTableOpen}{internalalias}{_syntaxsign.AliasEnclosureTableClose}.{on.internalkey.Description.Name} = {_syntaxsign.AliasEnclosureTableOpen}{externalalias}{_syntaxsign.AliasEnclosureTableClose}.{on.externalkey.Description.Name}";
                }
                from += $"{joinons}{Environment.NewLine}";
            }

            return from;
        }

        public virtual string 
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

        public virtual string 
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
