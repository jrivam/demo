using Library.Interface.Persistence.Table;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Presentation.Table;

namespace Library.Interface.Presentation.Raiser
{
    public interface IRaiserInteractive<T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        W CreateInstance(V domain, int maxdepth);

        W Clear(W presentation, int maxdepth = 1, int depth = 0);
        W Raise(W presentation, int maxdepth = 1, int depth = 0);

        W Extra(W presentation, int maxdepth = 1, int depth = 0);
    }
}