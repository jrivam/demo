using jrivam.Library.Interface.Persistence.Query;

namespace jrivam.Library.Interface.Business.Query
{
    public interface IQueryColumns
    {
        IColumnQuery this[string name] { get; }
    }
}
