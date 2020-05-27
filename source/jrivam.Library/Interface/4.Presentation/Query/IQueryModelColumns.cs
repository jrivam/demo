using jrivam.Library.Interface.Persistence.Query;

namespace jrivam.Library.Interface.Presentation.Query
{
    public interface IQueryModelColumns
    {
        IColumnQuery this[string name] { get; }
    }
}
