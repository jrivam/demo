using library.Interface.Data.Table;
using library.Interface.Entities;

namespace library.Interface.Domain.Table
{
    public interface IEntityLogicProperties<T, U> 
        where T: IEntity
        where U : IEntityRepositoryProperties<T>
    {
        U Data { get; }

        bool Changed { get; set; } 
        bool Deleted { get; set; }
    }
}
