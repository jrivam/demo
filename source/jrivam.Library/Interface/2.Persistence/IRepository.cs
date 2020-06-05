using jrivam.Library.Impl;
using jrivam.Library.Impl.Persistence.Sql;
using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Interface.Persistence
{
    public interface IRepository
    {
        (Result result, IEnumerable<T> entities) Select<T>(string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null, int maxdepth = 1, ICollection<T> entities = null);
        (Result result, object scalar) Insert(string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null);
        (Result result, int rows) Update(string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null);
        (Result result, int rows) Delete(string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null);
    }
}
