using library.Impl.Data.Sql;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Interface.Data.Sql
{
    public interface ISqlBuilderTable<T> : ISqlBuilder
        where T : IEntity
    {
        (string commandtext, IList<SqlParameter> parameters) Select(ITableRepositoryProperties<T> entitytable);

        (string commandtext, IList<SqlParameter> parameters) Insert(ITableRepositoryProperties<T> entitytable);
        (string commandtext, IList<SqlParameter> parameters) Update(ITableRepositoryProperties<T> entitytable);
        (string commandtext, IList<SqlParameter> parameters) Delete(ITableRepositoryProperties<T> entitytable);
    }
}
