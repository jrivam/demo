using library.Impl;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System.Collections.Generic;
using System.Data;

namespace library.Interface.Data
{
    public interface IDbRepository<T, U>
        where T : IEntity
        where U : ITableRepositoryProperties<T>
    {
        (Result result, IEnumerable<U> datas) ExecuteQuery(IDbCommand command, int maxdepth = 1, IList<U> datas = null);
    }
}
