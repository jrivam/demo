using Library.Impl.Persistence.Sql;
using System.Collections.Generic;

namespace Library.Interface.Persistence.Query
{
    public interface IColumnQuery : IColumn
    {
        IBuilderQueryData Query { get; }

        IList<(object value, WhereOperator? sign)> Wheres { get; set; }

        IColumnQuery Where((object value, WhereOperator? sign) condition);
        IColumnQuery Where(params (object value, WhereOperator? sign)[] conditions);

        IColumnQuery Where(object value, WhereOperator? sign = WhereOperator.Equals);
    }
}
