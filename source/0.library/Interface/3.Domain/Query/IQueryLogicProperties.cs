using library.Interface.Data.Query;

namespace library.Interface.Domain.Query
{
    public interface IQueryLogicProperties<S> 
        where S : IQueryRepositoryProperties
    {
        S Data { get; }
    }
}
