using library.Interface.Data.Sql;
using library.Interface.Data.Table;
using System;
using System.Collections.Generic;
using System.Data;

namespace library.Impl.Data.Sql.Builder
{
    public abstract class AbstractSqlBuilder : ISqlBuilder
    {
        protected readonly ISqlSyntaxSign _syntaxsign;

        public AbstractSqlBuilder(ISqlSyntaxSign syntaxsign)
        {
            _syntaxsign = syntaxsign;
        }

        public virtual SqlParameter 
            GetParameter
            (string name, Type type, object value, ParameterDirection direction = ParameterDirection.Input)
        {
            var parameter = new SqlParameter();
            parameter.Name = name;
            parameter.Type = type;
            parameter.Value = value;
            parameter.Direction = direction;

            return parameter;
        }
        public virtual IEnumerable<(ITableColumn column, SqlParameter parameter)> 
            GetParameters
            (IList<ITableColumn> columns, IList<SqlParameter> parameters)
        {
            foreach (var c in columns)
            {
                var parameter = GetParameter($"{_syntaxsign.ParameterPrefix}{c.TableDescription.Reference}{_syntaxsign.ParameterSeparator}{c.ColumnDescription.Reference}", c.Type, c.Value, ParameterDirection.Input);
                if (parameters.IndexOf(parameter) < 0)
                    parameters.Add(parameter);

                yield return (c, parameter);
            }
        }
        public virtual string 
            GetUpdateSet
            (IList<ITableColumn> columns, IList<SqlParameter> parameters, bool prefixtablename = true)
        {
            var set = string.Empty;

            foreach (var cp in GetParameters(columns, parameters))
            {
                set += $"{(string.IsNullOrWhiteSpace(set) ? "" : $",{Environment.NewLine}")}{(prefixtablename ? $"{cp.column.TableDescription.Name}." : string.Empty)}{cp.column.ColumnDescription.Name} = {cp.parameter.Name}";
            }
            set = $"{(!string.IsNullOrWhiteSpace(set) ? $"{set}{Environment.NewLine}" : "")}";

            return set;
        }
    }
}
