using Library.Impl.Data;

namespace Library.Interface.Data.Table
{
    public interface ITableDataColumns : IDescription
    {
        ListColumns<IColumnTable> Columns { get; set; }
        IColumnTable this[string reference] { get; }
    }
}
