using library.Impl.Data;
using library.Impl.Data.Repository;
using library.Interface.Data.Query;
using System;
using System.Collections.Generic;

namespace library.Interface.Data
{
    public interface IQueryColumn
    {
        Type Type { get; }

        IQueryRepositoryProperties Table { get; }

        Description Description { get; }

        IList<(object value, WhereOperator sign)> Wheres { get; set; }

        IQueryRepositoryProperties Where((object value, WhereOperator sign) condition);
        IQueryRepositoryProperties Where(params (object value, WhereOperator sign)[] conditions);

        IQueryRepositoryProperties Where(object value, WhereOperator sign = WhereOperator.Equals);
    }
}
