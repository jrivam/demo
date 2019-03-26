using Library.Interface.Data.Query;

namespace Library.Interface.Domain.Query
{
    public interface IQueryDomainColumns
    {
        IColumnQuery this[string reference] { get; }
    }
}
