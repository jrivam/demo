using Library.Interface.Data.Table;
using Library.Interface.Domain.Table;
using Library.Interface.Entities;

namespace Library.Interface.Domain.Mapper
{
    public interface IMapperLogic<T, U, V> 
        where T: IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        V CreateInstance(U data);

        V Clear(V domain, int maxdepth = 1, int depth = 0);
        V Load(V domain, int maxdepth = 1, int depth = 0);

        V Extra(V domain, int maxdepth = 1, int depth = 0);
    }
}