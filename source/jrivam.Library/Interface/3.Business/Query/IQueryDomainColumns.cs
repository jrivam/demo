using jrivam.Library.Interface.Persistence.Query;

namespace jrivam.Library.Interface.Business.Query
{
    public interface IQueryDomainColumns
    {
        IColumnQuery this[string name] { get; }
    }
}
