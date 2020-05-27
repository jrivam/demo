using jrivam.Library.Impl;
using System.Data;

namespace jrivam.Library.Interface.Persistence.Database
{
    public interface IDbCommandExecutorBulk
    {
        (Result result, int rows) ExecuteNonQuery(IDbCommand command);
        (Result result, object scalar) ExecuteScalar(IDbCommand command);
    }
}
