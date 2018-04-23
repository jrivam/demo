using library.Interface.Data.Query;

namespace library.Interface.Domain.Query
{
    public interface IQueryState<S> 
        where S : IQueryTable
    {
        S Data { get; }
    }
}
