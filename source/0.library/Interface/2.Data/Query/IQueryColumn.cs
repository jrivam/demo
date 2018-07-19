using library.Impl.Data.Definition;
using library.Impl.Data.Sql;
using System;
using System.Collections.Generic;

namespace library.Interface.Data.Query
{
    public interface IQueryColumn
    {
        Type Type { get; }

        Description Description { get; }

        IList<(object value, WhereOperator? sign)> Wheres { get; set; }

        void Where((object value, WhereOperator? sign) condition);
        void Where(params (object value, WhereOperator? sign)[] conditions);

        void Where(object value, WhereOperator? sign = WhereOperator.Equals);
    }
}
