﻿using library.Impl.Data.Definition;
using library.Impl.Data.Sql;
using System.Collections.Generic;

namespace library.Interface.Data.Query
{
    public interface IQueryRepositoryProperties
    {
        Description Description { get; }
        IList<(IQueryColumn internalkey, IQueryColumn externalkey)> GetJoins(int maxdepth = 1, int depth = 0);

        IList<(IQueryColumn column, OrderDirection flow)> Orders { get; }

        IList<IQueryColumn> Columns { get; }
        IQueryColumn this[string reference] { get; }

        void ClearWhere();
    }
}
