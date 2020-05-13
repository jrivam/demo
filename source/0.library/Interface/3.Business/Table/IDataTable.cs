using Library.Interface.Entities;
using Library.Interface.Persistence.Table;

namespace Library.Interface.Business.Table
{
    public interface IDataTable<T, U> 
        where T: IEntity
        where U : ITableData<T, U>
    {
        //T Entity { get; set; }

        U Data { get; set; }
    }
}
