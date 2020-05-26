using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;

namespace Library.Interface.Business.Loader
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