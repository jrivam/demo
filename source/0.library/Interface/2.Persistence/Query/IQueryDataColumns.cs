using Library.Impl.Persistence;

namespace Library.Interface.Persistence.Query
{
    public interface IQueryDataColumns : IDescription
    {
        ListColumns<IColumnQuery> Columns { get; }
        IColumnQuery this[string name] { get; }
    }
}
