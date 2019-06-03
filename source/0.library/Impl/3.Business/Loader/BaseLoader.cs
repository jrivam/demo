using Library.Interface.Business.Loader;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;

namespace Library.Impl.Domain.Loader
{
    public class BaseLoader<T, U, V> : ILoader<T, U, V> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        public BaseLoader()
        {
        }

        public virtual V Clear(V domain)
        {
            return domain;
        }

        public virtual V Load(V domain, int maxdepth = 1, int depth = 0)
        {
            return domain;
        }
        public virtual V LoadX(V domain, int maxdepth = 1, int depth = 0)
        {
            return domain;
        }
    }
}
