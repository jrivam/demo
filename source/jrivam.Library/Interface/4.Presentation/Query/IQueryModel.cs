using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation.Table;

namespace jrivam.Library.Interface.Presentation.Query
{
    public interface IQueryModel<T, U, V, W>: IBuilderQueryModel, IQueryModelMethods<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        IQueryDomain<T, U, V> Domain { get; set; }

        string Status { get; set; }
    }
}
