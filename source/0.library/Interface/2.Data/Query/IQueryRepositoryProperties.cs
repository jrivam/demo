using library.Impl.Data;
using library.Impl.Data.Repository;
using System.Collections.Generic;

namespace library.Interface.Data.Query
{
    public interface IQueryRepositoryProperties
    {
        Description Description { get; }

        IList<(IQueryColumn column, OrderDirection flow)> Orders { get; }

        IList<IQueryColumn> Columns { get; }
        IQueryColumn this[string reference] { get; }

        IList<(IQueryColumn internalkey, IQueryColumn externalkey)> Joins { get; }

        void ClearWhere();
    }
}
