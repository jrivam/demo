using Library.Interface.Entities;
using Library.Interface.Persistence.Table;

namespace Library.Interface.Persistence.Mapper
{
    public interface IMapper<T, U> 
        where T : IEntity
        where U : ITableData<T, U>
    {
        U Clear(U data);

        U Map(U data, int maxdepth = 1, int depth = 0);
        U MapX(U data, int maxdepth = 1, int depth = 0);
    }
}