using library.Impl.Data.Sql;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Interface.Data.Sql
{
    public interface ISqlBuilderTable<T> : ISqlBuilder
        where T : IEntity
    {
        (string commandtext, IList<SqlParameter> parameters) Select(IEntityRepositoryProperties<T> entitytable);

        (string commandtext, IList<SqlParameter> parameters) Insert(IEntityRepositoryProperties<T> entitytable);
        (string commandtext, IList<SqlParameter> parameters) Update(IEntityRepositoryProperties<T> entitytable);
        (string commandtext, IList<SqlParameter> parameters) Delete(IEntityRepositoryProperties<T> entitytable);
    }
}
