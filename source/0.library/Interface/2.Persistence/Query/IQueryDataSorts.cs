using Library.Impl.Persistence.Sql;
using System.Collections.Generic;

namespace Library.Interface.Persistence.Query
{
    public interface IQueryDataSorts
    {
        IList<(IColumnQuery column, OrderDirection flow)> Orders { get; }
    }
}
