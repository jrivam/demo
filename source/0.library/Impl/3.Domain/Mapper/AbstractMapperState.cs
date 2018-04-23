using library.Interface.Data.Model;
using library.Interface.Domain.Mapper;
using library.Interface.Domain.Model;
using library.Interface.Entities;

namespace library.Impl.Domain.Mapper
{
    public class AbstractMapperState<T, U, V> : IMapperState<T, U, V> 
        where T : IEntity
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
