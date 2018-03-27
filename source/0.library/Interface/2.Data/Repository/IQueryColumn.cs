using library.Impl.Data;
using System;
using System.Collections.Generic;

namespace library.Interface.Data.Repository
{
    public interface IQueryColumn
    {
        Type Type { get; }

        IQueryTable Table { get; }

        string Name { get; }
        string Reference { get; }

        IList<(object value, WhereOperator sign)> Wheres { get; set; }

        IQueryTable Where((object value, WhereOperator sign) condition);
        IQueryTable Where(params (object value, WhereOperator sign)[] conditions);

        IQueryTable Where(object value, WhereOperator sign = WhereOperator.Equals);
    }
}
