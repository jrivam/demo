using library.Interface.Data;
using library.Interface.Data.Repository;
using library.Interface.Data.Sql;
using library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace library.Impl.Data.Sql.Builder
{
    public abstract class AbstractSqlBuilder<T> : ISqlBuilder<T> where T : IEntity
    {
        public ISqlSyntaxSign SyntaxSign { get; set; }

        protected readonly IDbObjectCreator _objectcreator;

        protected abstract IDictionary<DbType, object> DbTypeToSqlType { get; }

        public abstract IDbCommand Select(IQueryTable query, int maxdepth = 1, int top = 0);

        public abstract IDbCommand Update(IEntityTable<T> entity, IQueryTable query, int maxdepth = 1);
        public abstract IDbCommand Delete(IQueryTable query, int maxdepth = 1);

        public abstract IDbCommand Select(IEntityTable<T> entity);

        public abstract IDbCommand Insert(IEntityTable<T> entity);
        public abstract IDbCommand Update(IEntityTable<T> entity);
        public abstract IDbCommand Delete(IEntityTable<T> entity);

        public AbstractSqlBuilder(ConnectionStringSettings connectionstringsettings, ISqlSyntaxSign syntaxsign)
        {
            _objectcreator = new DbObjectCreator(connectionstringsettings);
            SyntaxSign = syntaxsign;
        }

        public virtual IDbDataParameter GetParameter(string name, Type type, object value, ParameterDirection direction = ParameterDirection.Input)
        {
            var parameter = _objectcreator.Parameter;
            parameter.ParameterName = $"{SyntaxSign.ParameterPrefix}{name}";
            parameter.DbType = _objectcreator.TypeToDbType[type];
            parameter.Value = value ?? DBNull.Value;
            parameter.Direction = direction;

            return parameter;
        }
        public virtual IDbCommand GetCommand(string commandtext = "", CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null)
        {
            var command = _objectcreator.Command;

            command.CommandText = commandtext;
            command.CommandType = commandtype;

            parameters?.ToList()?.ForEach(p => 
            {
                command.Parameters.Add(GetParameter(p.Name, p.Type, p.Value, p.Direction));
            });

            command.Connection = _objectcreator.Connection;

            return command;
        }

        protected virtual string GetOperator(WhereOperator whereoperator)
        {
            var sign = string.Empty;

            switch (whereoperator)
            {
                case WhereOperator.NotEquals:
                case WhereOperator.Equals:
                    sign = "=";
                    break;
                case WhereOperator.NotGreater:
                case WhereOperator.Greater:
                    sign = ">";
                    break;
                case WhereOperator.NotLess:
                case WhereOperator.Less:
                    sign = "<";
                    break;
                case WhereOperator.EqualOrGreater:
                    sign = ">=";
                    break;
                case WhereOperator.EqualOrLess:
                    sign = "<=";
                    break;
                case WhereOperator.NotLikeBegin:
                case WhereOperator.NotLikeEnd:
                case WhereOperator.NotLike:
                case WhereOperator.LikeBegin:
                case WhereOperator.LikeEnd:
                case WhereOperator.Like:
                    sign = "like";
                    break;
                default:
                    break;
            }

            return sign;
        }

        protected virtual IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)> GetQueryColumns(IQueryTable query, IList<string> tablenames, IList<string> aliasnames, int maxdepth = 1, int depth = 0)
        {
            var columns = new List<(IQueryColumn, IList<string>, IList<string>)>();

            if (tablenames == null)
                tablenames = new List<string>();
            tablenames.Add(query.Name);

            if (aliasnames == null)
                aliasnames = new List<string>();
            aliasnames.Add(query.Reference);

            foreach (var c in query.Columns)
            {
                columns.Add((c, new List<string>(tablenames), new List<string>(aliasnames)));
            }

            depth++;
            if (depth < maxdepth || maxdepth == 0)
            {
                foreach (var j in query.Joins)
                {
                    columns.AddRange(GetQueryColumns(j.externalkey.Table, tablenames, aliasnames, maxdepth, depth));
                }
            }

            return columns;
        }
        protected virtual IList<(IQueryTable internaltable, string internalalias, IQueryTable externaltable, string externalalias, IList<(IQueryColumn, IQueryColumn)> joins)> GetQueryJoins(IQueryTable table, string prefix = "", int maxdepth = 1, int depth = 0, string tableseparator = "_")
        {
            var joins = new List<(IQueryTable, string, IQueryTable, string, IList<(IQueryColumn, IQueryColumn)>)>();

            depth++;
            if (depth < maxdepth || maxdepth == 0)
            {
                foreach (var j in table.Joins)
                {
                    var tablename = $"{(prefix == "" ? "" : $"{prefix}{tableseparator}")}{j.externalkey.Table.Name}";

                    joins.Add((table, prefix, j.externalkey.Table, tablename, new List<(IQueryColumn, IQueryColumn)>() { (j.internalkey, j.externalkey) }));

                    joins.AddRange(GetQueryJoins(j.externalkey.Table, tablename, maxdepth, depth, tableseparator));
                }
            }

            return joins;
        }

        public virtual IEnumerable<((object value, WhereOperator sign) where, IDbDataParameter parameter, int counter)> GetQueryParameters((IQueryColumn column, IList<string> aliases, IList<string> parameters) column, IDbCommand command)
        {
            var count = 0;
            foreach (var w in column.column.Wheres)
            {
                count++;

                var parameter = GetParameter($"{string.Join(SyntaxSign.ParameterSeparator, column.parameters)}{SyntaxSign.ParameterSeparator}{column.column.Reference}{count}", column.column.Type, w.value, ParameterDirection.Input);

                if ((w.sign & WhereOperator.LikeBegin) == WhereOperator.LikeBegin)
                    parameter.Value = $"%{parameter.Value}";
                if ((w.sign & WhereOperator.LikeEnd) == WhereOperator.LikeEnd)
                    parameter.Value = $"{parameter.Value}%";

                if (command.Parameters.IndexOf(parameter.ParameterName) < 0)
                    command.Parameters.Add(parameter);

                yield return (w, parameter, count);
            }
        }
        public virtual IEnumerable<(IEntityColumn<T> column, IDbDataParameter parameter)> GetEntityParameters(IList<IEntityColumn<T>> columns, IDbCommand command)
        {
            foreach (var c in columns)
            {
                var parameter = GetParameter($"{c.Table.Reference}{SyntaxSign.ParameterSeparator}{c.Reference}", c.Type, c.Value, ParameterDirection.Input);
                if (command.Parameters.IndexOf(parameter.ParameterName) < 0)
                    command.Parameters.Add(parameter);

                yield return (c, parameter);
            }
        }

        protected virtual string GetQueryFrom(IList<(IQueryTable internaltable, string internalalias, IQueryTable externaltable, string externalalias, IList<(IQueryColumn internalkey, IQueryColumn externalkey)> joins)> joins, string tablename)
        {
            var from = $"{tablename} {SyntaxSign.AliasSeparatorTableKeyword} {SyntaxSign.AliasEnclosureTableOpen}{tablename}{SyntaxSign.AliasEnclosureTableClose}{Environment.NewLine}";
            foreach (var j in joins)
            {
                from += $"left join {j.externaltable.Name} {SyntaxSign.AliasSeparatorTableKeyword} {SyntaxSign.AliasEnclosureTableOpen}{j.externalalias}{SyntaxSign.AliasEnclosureTableClose}{Environment.NewLine}on ";
                var joinons = string.Empty;
                foreach (var on in j.joins)
                {
                    joinons += $"{(string.IsNullOrWhiteSpace(joinons) ? "" : "and")} {SyntaxSign.AliasEnclosureTableOpen}{j.internalalias}{SyntaxSign.AliasEnclosureTableClose}.{on.internalkey.Name} = {SyntaxSign.AliasEnclosureTableOpen}{j.externalalias}{SyntaxSign.AliasEnclosureTableClose}.{on.externalkey.Name}";
                }
                from += $"{joinons}{Environment.NewLine}";
            }

            return from;
        }

        protected virtual string GetQueryWhere(IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)> columns, IDbCommand command)
        {
            var where = string.Empty;

            foreach (var c in columns)
            {
                if (c.column.Wheres.Count > 0)
                {
                    where += $"{(string.IsNullOrWhiteSpace(where) ? "where " : "and ")} (";

                    var columnname = $"{SyntaxSign.AliasEnclosureTableOpen}{string.Join(SyntaxSign.AliasSeparatorTable, c.tablenames)}{SyntaxSign.AliasEnclosureTableClose}.{c.column.Name}";

                    foreach (var p in GetQueryParameters(c, command))
                    {
                        where += $"{(p.counter > 1 ? " or " : "")}{((p.where.sign & WhereOperator.Not) == WhereOperator.Not ? "not " : "")}{columnname} {GetOperator(p.where.sign)} {p.parameter.ParameterName}{(p.where.value == null ? $" or ({p.parameter.ParameterName} is null and {columnname} is null)" : "")}";
                    }

                    where += $"){Environment.NewLine}";
                }
            }

            return where;
        }
        protected virtual string GetQuerySelectColumns(IList<(IQueryColumn column, IList<string> tablenames, IList<string> aliasnames)> columns, IDbCommand command)
        {
            var select = string.Empty;

            var lasttable = string.Empty;
            foreach(var c in columns)
            {
                var tablename = $"{string.Join(SyntaxSign.AliasSeparatorTable, c.tablenames)}";

                select += $"{(string.IsNullOrWhiteSpace(select) ? "" : $",{(lasttable == tablename ? " " : Environment.NewLine)}")}{SyntaxSign.AliasEnclosureTableOpen}{tablename}{SyntaxSign.AliasEnclosureTableClose}.{c.column.Name} {SyntaxSign.AliasSeparatorColumnKeyword} {SyntaxSign.AliasEnclosureColumnOpen}{string.Join(SyntaxSign.AliasSeparatorColumn, c.aliasnames)}{SyntaxSign.AliasSeparatorColumn}{c.column.Reference}{SyntaxSign.AliasEnclosureColumnClose}";

                lasttable = tablename;
            };
            select = $"{(!string.IsNullOrWhiteSpace(select) ? $"{select}{Environment.NewLine}" : "")}";

            return select;
        }

        protected virtual string GetEntityWhere(IList<IEntityColumn<T>> columns, IDbCommand command)
        {
            var where = string.Empty;

            foreach (var p in GetEntityParameters(columns, command))
            {
                where += $"{(string.IsNullOrWhiteSpace(where) ? "where" : "and")} {p.column.Table.Name}.{p.column.Name} {GetOperator(WhereOperator.Equals)} {p.parameter.ParameterName}{Environment.NewLine}";
            }

            return where;
        }
        protected virtual string GetEntitySelectColumns(IList<IEntityColumn<T>> columns, IDbCommand command)
        {
            var select = string.Empty;

            foreach (var c in columns)
            {
                select += $"{(string.IsNullOrWhiteSpace(select) ? "" : $",{Environment.NewLine}")}{c.Table.Name}.{c.Name} {SyntaxSign.AliasSeparatorColumnKeyword} {SyntaxSign.AliasEnclosureColumnOpen}{c.Table.Reference}{SyntaxSign.AliasSeparatorColumn}{c.Reference}{SyntaxSign.AliasEnclosureColumnClose}";
            }
            select = $"{(!string.IsNullOrWhiteSpace(select) ? $"{select}{Environment.NewLine}" : "")}";

            return select;
        }
        protected virtual string GetEntityUpdateSet(IList<IEntityColumn<T>> columns, IDbCommand command)
        {
            var set = string.Empty;

            foreach (var p in GetEntityParameters(columns, command))
            {
                set += $"{(string.IsNullOrWhiteSpace(set) ? "" : $",{Environment.NewLine}")}{p.column.Table.Name}.{p.column.Name} = {p.parameter.ParameterName}";
            }
            set = $"{(!string.IsNullOrWhiteSpace(set) ? $"{set}{Environment.NewLine}" : "")}";

            return set;
        }
        protected virtual string GetEntityInsertColumns(IList<IEntityColumn<T>> columns, IDbCommand command)
        {
            var insert = string.Empty;

            foreach (var c in columns)
            {
                insert += $"{(string.IsNullOrWhiteSpace(insert) ? "" : $",{Environment.NewLine}")}{c.Name}";
            }
            insert = $"{(!string.IsNullOrWhiteSpace(insert) ? $"({insert}){Environment.NewLine}" : "")}";

            return insert;
        }
        protected virtual string GetEntityInsertValues(IList<IEntityColumn<T>> columns, IDbCommand command)
        {
            var values = string.Empty;

            foreach (var p in GetEntityParameters(columns, command))
            {
                values += $"{(string.IsNullOrWhiteSpace(values) ? "" : $",{Environment.NewLine}")}{p.parameter.ParameterName}";
            }
            values = $"{(!string.IsNullOrWhiteSpace(values) ? $"({values}){Environment.NewLine}" : "")}";

            return values;
        }
    }
}
