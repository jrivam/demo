using library.Impl;
using library.Impl.Data.Sql;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System.Collections.Generic;
using System.Data;

namespace library.Interface.Data
{
    public interface IRepository<T, U>
        where T : IEntity
        where U : IEntityRepositoryProperties<T>
    {
        (Result result, IEnumerable<U> datas) ExecuteQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, int maxdepth = 1, IList<U> datas = null);
        (Result result, IEnumerable<U> datas) ExecuteQuery(IDbCommand command, int maxdepth = 1, IList<U> datas = null);
    }
}
