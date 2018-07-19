using library.Interface.Data.Query;
using library.Interface.Domain.Query;

namespace library.Interface.Presentation.Query
{
    public interface IQueryInteractive<S, R> 
        where S : IQueryRepository
        where R : IQueryLogic<S>                          
    {
        R Domain { get; }

        IQueryColumn this[string reference] { get; }
    }
}
