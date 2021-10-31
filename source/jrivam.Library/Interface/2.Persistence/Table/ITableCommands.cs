using jrivam.Library.Interface.Persistence.Sql;

namespace jrivam.Library.Interface.Persistence.Table
{
    public interface ITableCommands
    {
        (bool usedbcommand, ISqlCommand dbcommand)? SelectDbCommand { get; set; }
        (bool usedbcommand, ISqlCommand dbcommand)? InsertDbCommand { get; set; }
        (bool usedbcommand, ISqlCommand dbcommand)? UpdateDbCommand { get; set; }
        (bool usedbcommand, ISqlCommand dbcommand)? DeleteDbCommand { get; set; }
    }
}
