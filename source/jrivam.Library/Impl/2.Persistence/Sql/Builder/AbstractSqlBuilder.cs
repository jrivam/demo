using jrivam.Library.Interface.Persistence.Sql;
using jrivam.Library.Interface.Persistence.Sql.Builder;
using jrivam.Library.Interface.Persistence.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace jrivam.Library.Impl.Persistence.Sql.Builder
{
    public abstract class AbstractSqlBuilder : ISqlBuilder
    {
        protected readonly ISqlSyntaxSign _sqlsyntaxsign;

        public AbstractSqlBuilder(ISqlSyntaxSign sqlsyntaxsign)
        {
            _sqlsyntaxsign = sqlsyntaxsign;
        }

        public virtual IEnumerable<(Description table, Description column, ISqlParameter parameter)>
            GetParameters
            (IList<IColumnTable> columns, IList<ISqlParameter> parameters)
        {
            foreach (var c in columns)
            {
                var parameter = Helper.GetParameter($"{_sqlsyntaxsign.ParameterPrefix}{c.Table.Description.Name}{_sqlsyntaxsign.ParameterSeparator}{c.Description.Name}", c.Type, c.Value, ParameterDirection.Input);
                if (parameters.FirstOrDefault(x => x.Name == parameter.Name) == null)
                {
                    parameters.Add(parameter);
                }

                yield return (c.Table.Description, c.Description, parameter);
            }
        }

        public virtual string
            GetUpdateSet
            (IList<IColumnTable> columns, IList<ISqlParameter> parameters)
        {
            var set = string.Empty;

            foreach (var cp in GetParameters(columns, parameters))
            {
                set += $"{(string.IsNullOrWhiteSpace(set) ? string.Empty : $",{Environment.NewLine}")}{(_sqlsyntaxsign.UpdateSetUseAlias ? $"{cp.table.DbName}." : string.Empty)}{cp.column.DbName} = {cp.parameter.Name}";
            }
            set = $"{(!string.IsNullOrWhiteSpace(set) ? $"{set}{Environment.NewLine}" : string.Empty)}";

            return set;
        }
    }
}
