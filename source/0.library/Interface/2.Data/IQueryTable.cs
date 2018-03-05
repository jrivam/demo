using library.Impl.Data;
using library.Interface.Data.Repository;
using System.Collections.Generic;

namespace library.Interface.Data
{
    public interface IQueryTable
    {
        string Reference { get; }
        string Name { get; }

        IList<(IQueryColumn column, OrderDirection flow)> Orders { get; set; }

        IList<IQueryColumn> Columns { get; }
        IQueryColumn this[string reference] { get; }

        IList<(IQueryColumn internalkey, IQueryColumn externalkey)> Joins { get; }
    }
}
