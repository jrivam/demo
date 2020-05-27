using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Interface.Business.Table
{
    public interface IDataTable<T, U> 
        where T: IEntity
        where U : ITableData<T, U>
    {
        //T Entity { get; set; }

        U Data { get; set; }
    }
}
