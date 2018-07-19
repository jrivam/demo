using library.Impl;
using System.Data;

namespace library.Interface.Entities.Repository
{
    public interface IDbRepositoryBulk
    {
        (Result result, int rows) ExecuteNonQuery(IDbCommand command);
        (Result result, object scalar) ExecuteScalar(IDbCommand command);
    }
}
