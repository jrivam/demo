using Library.Interface.Persistence.Query;

namespace Library.Interface.Presentation.Query
{
    public interface IQueryModelColumns
    {
        IColumnQuery this[string reference] { get; }
    }
}
