using Library.Impl;
using System.Data;

namespace Library.Interface.Data.Database
{
    public interface IDbRepositoryBulk
    {
        (Result result, int rows) ExecuteNonQuery(IDbCommand command);
        (Result result, object scalar) ExecuteScalar(IDbCommand command);
    }
}
