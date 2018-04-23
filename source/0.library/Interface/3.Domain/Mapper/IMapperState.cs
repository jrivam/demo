using library.Interface.Data.Model;
using library.Interface.Domain.Model;
using library.Interface.Entities;

namespace library.Interface.Domain.Mapper
{
    public interface IMapperState<T, U, V> 
        where T: IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>
    {
        V Clear(V domain);
        V Map(V domain);
    }
}