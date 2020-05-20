using Library.Impl.Persistence.Sql;
using Library.Interface.Persistence.Query;
using System.Collections.Generic;
using System.Linq;

namespace Library.Impl.Persistence.Query
{
    public class ColumnQuery<A> : Column<A>, IColumnQuery
    {
        public virtual IBuilderQueryData Query { get; protected set; }

        public virtual IList<(object value, WhereOperator? sign)> Wheres { get; set; } = new List<(object, WhereOperator?)>();

        public ColumnQuery(IBuilderQueryData query,
            string name, string dbname)
            : base(name, dbname)
        {
            Query = query;
        }

        public virtual IColumnQuery Where((object value, WhereOperator? sign) condition)
        {
            if (!Wheres.Contains(condition))
                Wheres?.Add(condition);

            return this;
        }
        public virtual IColumnQuery Where(params (object value, WhereOperator? sign)[] conditions)
        {
            conditions?.ToList()?.ForEach(x => Where(x));

            return this;
        }
        public virtual IColumnQuery Where(object value, WhereOperator? sign = WhereOperator.Equals)
        {
            if (value is IList<object>)
                return Where(((IList<object>)value).Select(x => (x, sign)).ToArray());
            else
                return Where((value, sign));
        }
        public virtual IColumnQuery Where(IList<object> value, WhereOperator sign = WhereOperator.Equals)
        {
            return Where(value.Select(x => (x, sign)).ToArray());
        }
    }
}
