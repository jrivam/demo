using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Interface.Persistence.Mapper
{
    public interface IMapper<T, U> 
        where T : IEntity
        where U : ITableData<T, U>
    {
        void Clear(U data);

        void Map(U data, int maxdepth = 1, int depth = 0);
    }
}