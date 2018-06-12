using library.Interface.Data;
using library.Interface.Data.Sql;
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

        public virtual string GetOperator(WhereOperator whereoperator)
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

        public virtual SqlParameter GetParameter(string name, Type type, object value, ParameterDirection direction = ParameterDirection.Input)
        {
            var parameter = new SqlParameter();
            parameter.Name = name;
            parameter.Type = type;
            parameter.Value = value;
            parameter.Direction = direction;

            return parameter;
        }
        public virtual IEnumerable<(IEntityColumn column, SqlParameter parameter)> GetParameters(IList<IEntityColumn> columns, IList<SqlParameter> parameters)
        {
            foreach (var c in columns)
            {
                var parameter = GetParameter($"{_syntaxsign.ParameterPrefix}{c.TableDescription.Reference}{_syntaxsign.ParameterSeparator}{c.ColumnDescription.Reference}", c.Type, c.Value, ParameterDirection.Input);
                if (parameters.IndexOf(parameter) < 0)
                    parameters.Add(parameter);

                yield return (c, parameter);
            }
        }
        public virtual string GetUpdateSet(IList<IEntityColumn> columns, IList<SqlParameter> parameters)
        {
            var set = string.Empty;

            foreach (var cp in GetParameters(columns, parameters))
            {
                set += $"{(string.IsNullOrWhiteSpace(set) ? "" : $",{Environment.NewLine}")}{cp.column.TableDescription.Name}.{cp.column.ColumnDescription.Name} = {cp.parameter.Name}";
            }
            set = $"{(!string.IsNullOrWhiteSpace(set) ? $"{set}{Environment.NewLine}" : "")}";

            return set;
        }
    }
}
