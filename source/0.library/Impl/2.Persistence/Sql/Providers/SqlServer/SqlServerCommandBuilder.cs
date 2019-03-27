using Library.Interface.Persistence.Sql.Providers;
using System;

namespace Library.Impl.Persistence.Sql.Providers.SqlServer
{
    public class SqlServerCommandBuilder : ISqlCommandBuilder
    {
        public SqlServerCommandBuilder()
        {
        }

        public string Select(string columns, string from, string where, int top = 0)
        {
            var commandtext = string.Empty;

            commandtext = $"select{Environment.NewLine}{columns}{Environment.NewLine}from {from}{Environment.NewLine}{where}";
            commandtext = commandtext.Replace("select", $"select{(top > 0 ? $" top {top.ToString()}" : string.Empty)}");

            return commandtext;
        }
        public string Insert(string into, string insert, string values, string output)
        {
            var commandtext = string.Empty;

            commandtext = $"insert{Environment.NewLine}into {into}{Environment.NewLine}{insert}{Environment.NewLine}values {values}";
            commandtext += $";{Environment.NewLine}SELECT{output}SCOPE_IDENTITY()";

            return commandtext;
        }
        public string Update(string table, string from, string set, string where)
        {
            var commandtext = string.Empty;

            commandtext = $"update{Environment.NewLine}{table}{Environment.NewLine}set {set}{Environment.NewLine}from {from}{Environment.NewLine}{where}";

            return commandtext;
        }
        public string Delete(string table, string from, string where)
        {
            var commandtext = string.Empty;

            commandtext = $"delete {table}{Environment.NewLine}from {from}{Environment.NewLine}{where}";

            return commandtext;
        }
    }
}
