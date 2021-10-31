using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Interface.Business.Loader
{
    public interface IDomainLoader
    {
        void Clear<T, U, V>(V domain)
            where T : IEntity
            where U : ITableData<T, U>
            where V : ITableDomain<T, U, V>;

        void Load<T, U, V>(V domain, int maxdepth = 1, int depth = 0)
            where T : IEntity
            where U : ITableData<T, U>
            where V : ITableDomain<T, U, V>;
    }
}