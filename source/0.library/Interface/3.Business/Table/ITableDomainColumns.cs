using Library.Interface.Data.Table;

namespace Library.Interface.Domain.Table
{
    public interface ITableDomainColumns
    {
        IColumnTable this[string reference] { get; }
    }
}
