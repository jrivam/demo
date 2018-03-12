using library.Interface.Business;
using library.Interface.Data;
using library.Interface.Domain;

namespace library.Impl.Business
{
    public class MapperState<T, U, V> : IMapperState<T, U, V> where T : IEntity
                                                            where U : IEntityTable<T>
                                                            where V : IEntityState<T, U>
    {
        public virtual V Clear(V business, int maxdepth = 1, int depth = 0)
        {
            return business;
        }

        public virtual V Map(V business, int maxdepth = 1, int depth = 0)
        {
            return business;
        }
    }
}
