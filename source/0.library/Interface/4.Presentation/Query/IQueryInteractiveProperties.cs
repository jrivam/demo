using library.Interface.Data;
using library.Interface.Data.Query;
using library.Interface.Domain.Query;

namespace library.Interface.Presentation.Query
{
    public interface IQueryInteractiveProperties<S, R> 
        where S : IQueryRepositoryProperties
        where R : IQueryLogicProperties<S>                          
    {
        R Domain { get; }

        IQueryColumn this[string reference] { get; }
    }
}
