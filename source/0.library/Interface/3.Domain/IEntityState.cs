using library.Interface.Data;
using library.Interface.Entities;

namespace library.Interface.Domain
{
    public interface IEntityState<T, U> where T: IEntity
                                        where U : IEntityTable<T>
    {
        U Data { get; set; }

        bool Loaded { get; set; }
        bool Changed { get; set; } 
        bool Deleted { get; set; }
    }
}
