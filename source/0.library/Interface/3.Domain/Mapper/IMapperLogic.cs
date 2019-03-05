using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;

namespace library.Interface.Domain.Mapper
{
    public interface IMapperLogic<T, U, V> 
        where T: IEntity
        where U : ITableRepository, ITableEntity<T>
        where V : ITableLogic<T, U>
    {
        V CreateInstance(U data);

        V Clear(V domain, int maxdepth = 1, int depth = 0);
        V Load(V domain, int maxdepth = 1, int depth = 0);

        V Extra(V domain, int maxdepth = 1, int depth = 0);
    }
}