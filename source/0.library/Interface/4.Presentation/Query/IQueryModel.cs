using Library.Interface.Business.Query;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Table;
using Library.Interface.Presentation.Table;

namespace Library.Interface.Presentation.Query
{
    public interface IQueryModel<R, S, T, U, V, W>: IBuilderQueryModel, IDomainQuery<R, S, T, U, V>, IQueryModelMethods<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
        where S : IQueryData<T, U>
        where R : IQueryDomain<S, T, U, V>
    {
        string Status { get; set; }
    }
}
