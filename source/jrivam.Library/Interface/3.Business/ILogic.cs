using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace jrivam.Library.Interface.Business
{
    public interface ILogic<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        (Result result, U data) Load(U data, bool usedbcommand = false);
        (Result result, U data) LoadQuery(U data, int maxdepth = 1);
        (Result result, IEnumerable<U> datas) List(IQueryData<T, U> query, int maxdepth = 1, int top = 0);
        (Result result, U data) Save(U data, bool useinsertdbcommand = false, bool useupdatedbcommand = false);
        (Result result, U data) Erase(U data, bool usedbcommand = false);
    }
}
