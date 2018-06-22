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
        V Clear(V domain);
        V Map(V domain);
    }
}