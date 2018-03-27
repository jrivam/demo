using library.Interface.Data;
using library.Interface.Domain;
using library.Interface.Entities;

namespace library.Impl.Domain
{
    public class MapperState<T, U, V> : IMapperState<T, U, V> where T : IEntity
                                                            where U : IEntityTable<T>
                                                            where V : IEntityState<T, U>
    {
        public virtual V Clear(V domain)
        {
            return domain;
        }

        public virtual V Map(V domain)
        {
            return domain;
        }
    }
}
