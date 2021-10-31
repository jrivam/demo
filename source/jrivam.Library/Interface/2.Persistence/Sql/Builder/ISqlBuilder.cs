using jrivam.Library.Impl.Persistence;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace jrivam.Library.Interface.Persistence.Sql.Builder
{
    public interface ISqlBuilder
    {
        IEnumerable<(Description table, Description column, ISqlParameter parameter)>
            GetParameters
           (IList<IColumnTable> columns, IList<ISqlParameter> parameters);

        string
            GetUpdateSet
            (IList<IColumnTable> columns, IList<ISqlParameter> parameters);
    }
}
