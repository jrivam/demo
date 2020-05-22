using Library.Interface.Persistence.Query;

namespace Library.Interface.Business.Query
{
    public interface IQueryDomainColumns
    {
        IColumnQuery this[string name] { get; }
    }
}
