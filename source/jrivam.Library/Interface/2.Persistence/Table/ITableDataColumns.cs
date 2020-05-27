using jrivam.Library.Impl.Persistence;

namespace jrivam.Library.Interface.Persistence.Table
{
    public interface ITableDataColumns : IDescription
    {
        ListColumns<IColumnTable> Columns { get; set; }
        IColumnTable this[string name] { get; }
    }
}
