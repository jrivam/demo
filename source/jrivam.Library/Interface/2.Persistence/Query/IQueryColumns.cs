using jrivam.Library.Impl.Persistence;

namespace jrivam.Library.Interface.Persistence.Query
{
    public interface IQueryColumns : IDescription
    {
        ListColumns<IColumnQuery> Columns { get; }
        IColumnQuery this[string name] { get; }
    }
}
