using Library.Impl;
using Library.Impl.Persistence.Sql;
using Library.Interface.Entities;
using System.Collections.Generic;
using System.Data;

namespace Library.Interface.Persistence
{
    public interface IRepository<T>
        where T : IEntity
    {
        (Result result, IEnumerable<T> entities) Select(string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null, int maxdepth = 1, ICollection<T> entities = null);
        (Result result, object scalar) Insert(string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null);
        (Result result, int rows) Update(string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null);
        (Result result, int rows) Delete(string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null);
    }
}
