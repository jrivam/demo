using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation.Table;

namespace library.Interface.Presentation.Raiser
{
    public interface IRaiserInteractive<T, U, V, W> 
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where V : ITableLogic<T, U>
        where W : ITableInteractive<T, U, V>
    {
        W CreateInstance(V domain, int maxdepth);

        W Clear(W presentation, int maxdepth = 1, int depth = 0);
        W Raise(W presentation, int maxdepth = 1, int depth = 0);

        W Extra(W presentation, int maxdepth = 1, int depth = 0);
    }
}