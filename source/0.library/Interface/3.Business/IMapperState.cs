using library.Interface.Data;
using library.Interface.Domain;

namespace library.Interface.Business
{
    public interface IMapperState<T, U, V> where T: IEntity
                                        where U : IEntityTable<T>
                                        where V : IEntityState<T, U>
    {
        V Clear(V business);
        V Map(V business);
    }
}