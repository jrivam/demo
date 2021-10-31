namespace jrivam.Library.Interface.Persistence.Sql.Providers
{
    public interface ISqlCommandBuilder
    {
        string Select(string columns, string from, string where, int top = 0);

        string Insert(string into, string insert, string values, bool output = false);

        string Update(string table, string from, string set, string where);

        string Delete(string table, string from, string where);
    }
}
