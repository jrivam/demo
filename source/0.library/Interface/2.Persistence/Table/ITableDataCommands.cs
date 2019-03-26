using Library.Impl.Data.Sql;
using System.Collections.Generic;
using System.Data;

namespace Library.Interface.Data.Table
{
    public interface ITableDataCommands
    {
        bool UseDbCommand { get; set; }
        (bool usedbcommand, (string text, CommandType type, IList<SqlParameter> parameters) dbcommand)? SelectDbCommand { get; set; }
        (bool usedbcommand, (string text, CommandType type, IList<SqlParameter> parameters) dbcommand)? InsertDbCommand { get; set; }
        (bool usedbcommand, (string text, CommandType type, IList<SqlParameter> parameters) dbcommand)? UpdateDbCommand { get; set; }
        (bool usedbcommand, (string text, CommandType type, IList<SqlParameter> parameters) dbcommand)? DeleteDbCommand { get; set; }
    }
}
