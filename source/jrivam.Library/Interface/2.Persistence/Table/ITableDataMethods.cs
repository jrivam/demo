using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using System.Data;

namespace jrivam.Library.Interface.Persistence.Table
{
    public interface ITableDataMethods<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        (Result result, U data) SelectQuery(int? commandtimeout = null, int maxdepth = 1, IDbConnection connection = null);
        (Result result, U data) Select(bool usedbcommand = false, int? commandtimeout = null, IDbConnection connection = null);

        (Result result, U data) Insert(bool usedbcommand = false, int? commandtimeout = null, IDbConnection connection = null, IDbTransaction transaction = null);
        (Result result, U data) Update(bool usedbcommand = false, int? commandtimeout = null, IDbConnection connection = null, IDbTransaction transaction = null);
        (Result result, U data) Delete(bool usedbcommand = false, int? commandtimeout = null, IDbConnection connection = null, IDbTransaction transaction = null);

        (Result result, U data) Upsert(bool usedbcommand = false, int? commandtimeout = null, IDbConnection connection = null, IDbTransaction transaction = null);
    }
}
