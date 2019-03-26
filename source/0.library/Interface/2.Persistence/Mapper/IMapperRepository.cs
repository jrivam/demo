using Library.Interface.Data.Table;
using Library.Interface.Entities;

namespace Library.Interface.Data.Mapper
{
    public interface IMapperRepository<T, U> 
        where T : IEntity
        where U : ITableData<T, U>
    {
        U CreateInstance(T entity);

        U Clear(U data, int maxdepth = 1, int depth = 0);
        U Map(U data, int maxdepth = 1, int depth = 0);

        U Extra(U data, int maxdepth = 1, int depth = 0);
    }
}