using library.Interface.Data.Table;
using library.Interface.Domain.Mapper;
using library.Interface.Domain.Table;
using library.Interface.Entities;

namespace library.Impl.Domain.Mapper
{
    public class BaseMapperLogic<T, U, V> : IMapperLogic<T, U, V> 
        where T : IEntity
        where U : ITableRepositoryProperties<T>
        where V : IEntityLogicProperties<T, U>
    {
        public virtual V Clear(V domain, int maxdepth = 1, int depth = 0)
        {
            return domain;
        }
    }
}
