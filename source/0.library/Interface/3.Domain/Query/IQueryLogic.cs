using library.Interface.Data;
using library.Interface.Data.Query;

namespace library.Interface.Domain.Query
{
    public interface IQueryLogic<S> 
        where S : IQueryRepository
    {
        S Data { get; }

        IQueryColumn this[string reference] { get; }
    }
}
