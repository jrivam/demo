using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Interface.Business
{
    public interface ILogic<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        (Result result, U data) Load(U data, bool usedbcommand = false, int? commandtimeout = null, IDbConnection connection = null);
        (Result result, U data) LoadQuery(U data, int? commandtimeout = null, int maxdepth = 1, IDbConnection connection = null);
        (Result result, IEnumerable<U> datas) List(IQueryData<T, U> query, int? commandtimeout = null, int maxdepth = 1, int top = 0, IDbConnection connection = null);

        (Result result, U data) Save(U data, bool useinsertdbcommand = false, bool useupdatedbcommand = false, int? commandtimeout = null, IDbConnection connection = null, IDbTransaction transaction = null);
        (Result result, U data) Erase(U data, bool usedbcommand = false, int? commandtimeout = null, IDbConnection connection = null, IDbTransaction transaction = null);
    }
}
