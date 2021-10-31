using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Interface.Persistence.Table
{
    public interface ITableDataMethodsAuto<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        (Result result, U data) SelectQuery(int? commandtimeout = null, int maxdepth = 1, IDbConnection connection = null);

        (Result result, U data) Select(int? commandtimeout = null, IDbConnection connection = null);

        (Result result, U data) Insert(int? commandtimeout = null, IDbConnection connection = null, IDbTransaction transaction = null);

        (Result result, U data) Update(int? commandtimeout = null, IDbConnection connection = null, IDbTransaction transaction = null);

        (Result result, U data) Delete(int? commandtimeout = null, IDbConnection connection = null, IDbTransaction transaction = null);

        (Result result, U data) Upsert(int? commandtimeout = null, IDbConnection connection = null, IDbTransaction transaction = null);
    }
}
