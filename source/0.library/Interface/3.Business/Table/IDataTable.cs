using Library.Interface.Data.Table;
using Library.Interface.Entities;

namespace Library.Interface.Domain.Table
{
    public interface IDataTable<T, U> 
        where T: IEntity
        where U : ITableData<T, U>
    {
        U Data { get; set; }
    }
}
