using library.Interface.Data;
using library.Interface.Domain;

namespace library.Interface.Presentation
{
    public interface IQueryView<T, U> where T : IQueryTable
                                        where U : IQueryState<T>
                            
    {
        U Domain { get; }
    }
}
