using library.Interface.Data.Table;
using library.Interface.Entities;

namespace library.Interface.Data.Mapper
{
    public interface IMapperRepository<T, U> 
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
    {
        U CreateInstance(T entity);

        U Clear(U data, int maxdepth = 1, int depth = 0);
        U Map(U data, int maxdepth = 1, int depth = 0);

        U Extra(U data, int maxdepth = 1, int depth = 0);
    }
}