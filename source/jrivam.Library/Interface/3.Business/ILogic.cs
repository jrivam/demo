using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Interface.Business
{
    public interface ILogic<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        (Result result, U data) Load(U data, bool usedbcommand = false);
        (Result result, U data) LoadQuery(IQueryData<T, U> query, U data, int maxdepth = 1);
        (Result result, U data) Save(U data, bool useinsertdbcommand = false, bool useupdatedbcommand = false);
        (Result result, U data) Erase(U data, bool usedbcommand = false);
    }
}
