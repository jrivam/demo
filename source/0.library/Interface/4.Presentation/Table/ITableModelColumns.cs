using Library.Interface.Data.Table;

namespace Library.Interface.Presentation.Table
{
    public interface ITableModelColumns
    {
        IColumnTable this[string reference] { get; }
    }
}
