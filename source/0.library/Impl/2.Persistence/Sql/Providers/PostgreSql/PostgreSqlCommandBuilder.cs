using Library.Interface.Persistence.Sql.Providers;
using System;

namespace Library.Impl.Persistence.Sql.Providers.PostgreSql
{
    public class PostgreSqlCommandBuilder : ISqlCommandBuilder
    {
        public PostgreSqlCommandBuilder()
        {
        }

        public string Select(string columns, string from, string where, int top = 0)
        {
            var commandtext = string.Empty;

            commandtext = $"select{Environment.NewLine}{columns}{Environment.NewLine}from {from}{Environment.NewLine}{where}";
            commandtext += $"{(top > 0 ? $" limit {top.ToString()}" : string.Empty)}";

            return commandtext;
        }
        public string Insert(string into, string insert, string values, string output)
        {
            var commandtext = string.Empty;

            commandtext = $"insert{Environment.NewLine}into {into}{Environment.NewLine}{insert}{Environment.NewLine}values {values}";
            commandtext += $";{Environment.NewLine}SELECT{output}lastval()";

            return commandtext;
        }
        public string Update(string table, string from, string set, string where)
        {
            var commandtext = string.Empty;

            commandtext = $"update{Environment.NewLine}{table}{Environment.NewLine}set {set}{Environment.NewLine}{where}";

            return commandtext;
        }
        public string Delete(string table, string from, string where)
        {
            var commandtext = string.Empty;

            commandtext = $"delete {Environment.NewLine}from {from}{Environment.NewLine}{where}";

            return commandtext;
        }
    }
}
