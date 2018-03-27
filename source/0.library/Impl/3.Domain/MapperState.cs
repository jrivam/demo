using library.Interface.Data;
using library.Interface.Domain;
using library.Interface.Entities;

namespace library.Impl.Business
{
    public class MapperState<T, U, V> : IMapperState<T, U, V> where T : IEntity
                                                            where U : IEntityTable<T>
                                                            where V : IEntityState<T, U>
    {
        public virtual V Clear(V business)
        {
            return business;
        }

        public virtual V Map(V business)
        {
            return business;
        }
    }
}
