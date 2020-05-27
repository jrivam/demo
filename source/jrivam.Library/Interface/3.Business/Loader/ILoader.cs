using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Interface.Business.Loader
{
    public interface ILoader<T, U, V> 
        where T: IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        void Clear(V domain);

        void Load(V domain, int maxdepth = 1, int depth = 0);
    }
}