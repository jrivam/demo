using library.Interface.Business;
using library.Interface.Data;

namespace library.Interface.Presentation
{
    public interface IQueryView<T, U> where T : IQueryTable
                                        where U : IQueryState<T>
                            
    {
        U Business { get; set; }
    }
}
