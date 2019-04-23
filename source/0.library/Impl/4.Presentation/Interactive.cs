using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;
using Library.Interface.Presentation;
using Library.Interface.Presentation.Table;

namespace Library.Impl.Presentation
{
    public class Interactive<T, U, V, W> : IInteractive<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        public Interactive()
        {
        }
    }
}
