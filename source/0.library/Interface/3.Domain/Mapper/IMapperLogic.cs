using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;

namespace library.Interface.Domain.Mapper
{
    public interface IMapperLogic<T, U, V> 
        where T: IEntity
        where U : ITableRepositoryProperties<T>
        where V : IEntityLogicProperties<T, U>
    {
        V Clear(V domain, int maxdepth = 1, int depth = 0);
    }
}