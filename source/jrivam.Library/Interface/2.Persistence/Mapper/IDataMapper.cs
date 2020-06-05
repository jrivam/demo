using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Interface.Persistence.Mapper
{
    public interface IDataMapper
    {
        void Clear<T, U>(U data)
            where T : IEntity
            where U : ITableData<T, U>;

        void Map<T, U>(U data, int maxdepth = 1, int depth = 0)
            where T : IEntity
            where U : ITableData<T, U>;
    }
}