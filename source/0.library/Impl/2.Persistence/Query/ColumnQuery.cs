using Library.Impl.Data.Sql;
using Library.Interface.Data.Query;
using System.Collections.Generic;
using System.Linq;

namespace Library.Impl.Data.Query
{
    public class ColumnQuery<A> : Column<A>, IColumnQuery
    {
        public virtual IBuilderQueryData Query { get; protected set; }

        public virtual IList<(object value, WhereOperator? sign)> Wheres { get; set; } = new List<(object, WhereOperator?)>();

        public ColumnQuery(IBuilderQueryData query,
            string name, string reference)
            : base(name, reference)
        {
            Query = query;
        }

        public virtual void Where((object value, WhereOperator? sign) condition)
        {
            if (!Wheres.Contains(condition))
                Wheres?.Add(condition);
        }
        public virtual void Where(params (object value, WhereOperator? sign)[] conditions)
        {
            conditions?.ToList()?.ForEach(x => Where(x));
        }
        public virtual void Where(object value, WhereOperator? sign = WhereOperator.Equals)
        {
            if (value is IList<object>)
                Where(((IList<object>)value).Select(x => (x, sign)).ToArray());
            else
                Where((value, sign));
        }
        public virtual void Where(IList<object> value, WhereOperator sign = WhereOperator.Equals)
        {
            Where(value.Select(x => (x, sign)).ToArray());
        }
    }
}
