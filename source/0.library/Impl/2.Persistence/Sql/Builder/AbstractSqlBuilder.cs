using Library.Interface.Persistence.Sql.Builder;
using System;
using System.Collections.Generic;
using System.Data;

namespace Library.Impl.Persistence.Sql.Builder
{
    public abstract class AbstractSqlBuilder : ISqlBuilder
    {
        protected readonly ISqlSyntaxSign _sqlsyntaxsign;

        public AbstractSqlBuilder(ISqlSyntaxSign sqlsyntaxsign)
        {
            _sqlsyntaxsign = sqlsyntaxsign;
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

        public virtual IEnumerable<(Description view, Description column, SqlParameter parameter)>
            GetParameters
            (IList<(Description view, Description column, Type type, object value)> columns, IList<SqlParameter> parameters)
        {
            foreach (var c in columns)
            {
                var parameter = GetParameter($"{_sqlsyntaxsign.ParameterPrefix}{c.view.Reference}{_sqlsyntaxsign.ParameterSeparator}{c.column.Reference}", c.type, c.value, ParameterDirection.Input);
                if (parameters.IndexOf(parameter) < 0)
                    parameters.Add(parameter);

                yield return (c.view, c.column, parameter);
            }
        }

        public virtual string
            GetUpdateSet
            (IList<(Description view, Description column, Type type, object value)> columns, IList<SqlParameter> parameters)
        {
            var set = string.Empty;

            foreach (var cp in GetParameters(columns, parameters))
            {
                set += $"{(string.IsNullOrWhiteSpace(set) ? string.Empty : $",{Environment.NewLine}")}{(_sqlsyntaxsign.UpdateSetUseAlias ? $"{cp.view.Name}." : string.Empty)}{cp.column.Name} = {cp.parameter.Name}";
            }
            set = $"{(!string.IsNullOrWhiteSpace(set) ? $"{set}{Environment.NewLine}" : string.Empty)}";

            return set;
        }
    }
}
