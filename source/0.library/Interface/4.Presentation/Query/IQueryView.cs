using library.Interface.Data.Query;
using library.Interface.Domain.Query;

namespace library.Interface.Presentation.Query
{
    public interface IQueryView<S, R> 
        where S : IQueryTable
        where R : IQueryState<S>                          
    {
        R Domain { get; }
    }
}
