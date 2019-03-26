using Library.Impl.Data;

namespace Library.Interface.Data.Query
{
    public interface IQueryDataColumns : IDescription
    {
        ListColumns<IColumnQuery> Columns { get; }
        IColumnQuery this[string reference] { get; }
    }
}
