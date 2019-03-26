using Library.Impl.Data.Sql;
using System.Collections.Generic;

namespace Library.Interface.Data.Query
{
    public interface IQueryDataSorts
    {
        IList<(IColumnQuery column, OrderDirection flow)> Orders { get; }
    }
}
