using Library.Interface.Persistence.Table;

namespace Library.Interface.Presentation.Table
{
    public interface ITableModelColumns
    {
        IColumnTable this[string reference] { get; }
    }
}
