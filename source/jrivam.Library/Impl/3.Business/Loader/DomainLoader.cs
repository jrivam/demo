using jrivam.Library.Interface.Business.Loader;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Impl.Business.Loader
{
    public class DomainLoader : IDomainLoader
    {
        public DomainLoader()
        {
        }

        public virtual void Clear<T, U, V>(V domain)
            where T : IEntity
            where U : ITableData<T, U>
            where V : ITableDomain<T, U, V>
        {
        }

        public virtual void Load<T, U, V>(V domain, int maxdepth = 1, int depth = 0)
            where T : IEntity
            where U : ITableData<T, U>
            where V : ITableDomain<T, U, V>
        {
        }
    }
}
