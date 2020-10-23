using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Interface.Persistence.Table
{
    public interface ITableDataMethodsCommand<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        (Result result, U data) SelectCommand(IDbConnection connection = null);

        (Result result, U data) InsertCommand(IDbConnection connection = null, IDbTransaction transaction = null);

        (Result result, U data) UpdateCommand(IDbConnection connection = null, IDbTransaction transaction = null);

        (Result result, U data) DeleteCommand(IDbConnection connection = null, IDbTransaction transaction = null);

        (Result result, U data) UpsertCommand(IDbConnection connection = null, IDbTransaction transaction = null);
    }
}
