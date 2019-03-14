using library.Interface.Data.Table;
using library.Interface.Entities;

namespace library.Interface.Domain.Table
{
    public interface ITableLogic<T, U> 
        where T: IEntity
        where U : ITableRepository, ITableEntity<T>
    {
        U Data { get; set; }

        ITableColumn this[string reference] { get; }

        bool Changed { get; set; } 
        bool Deleted { get; set; }
    }
}
