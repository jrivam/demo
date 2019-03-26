using Library.Impl.Data.Sql;
using System.Collections.Generic;

namespace Library.Interface.Data.Query
{
    public interface IColumnQuery : IColumn
    {
        IBuilderQueryData Query { get; }

        IList<(object value, WhereOperator? sign)> Wheres { get; set; }

        void Where((object value, WhereOperator? sign) condition);
        void Where(params (object value, WhereOperator? sign)[] conditions);

        void Where(object value, WhereOperator? sign = WhereOperator.Equals);
    }
}
