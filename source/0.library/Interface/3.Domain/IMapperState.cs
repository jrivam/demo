using library.Interface.Data;
using library.Interface.Entities;

namespace library.Interface.Domain
{
    public interface IMapperState<T, U, V> where T: IEntity
                                        where U : IEntityTable<T>
                                        where V : IEntityState<T, U>
    {
        V Clear(V domain);
        V Map(V domain);
    }
}