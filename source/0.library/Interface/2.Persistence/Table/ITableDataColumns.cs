using Library.Impl.Persistence;
using Library.Interface.Persistence;

namespace Library.Interface.Persistence.Table
{
    public interface ITableDataColumns : IDescription
    {
        ListColumns<IColumnTable> Columns { get; set; }
        IColumnTable this[string reference] { get; }
    }
}
