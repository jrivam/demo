using Library.Impl;
using Library.Interface.Data.Table;
using Library.Interface.Entities;

namespace Library.Interface.Domain.Table
{
    public interface ITableDomainMethods<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        (Result result, V domain) Load(bool usedbcommand = false);
        (Result result, V domain) LoadQuery(int maxdepth = 1);
        (Result result, V domain) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false);
        (Result result, V domain) Erase(bool usedbcommand = false);
    }
}
