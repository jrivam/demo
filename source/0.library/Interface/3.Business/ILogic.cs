using Library.Interface.Data.Table;
using Library.Interface.Domain.Table;
using Library.Interface.Entities;

namespace Library.Interface.Domain
{
    public interface ILogic<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
    }
}
