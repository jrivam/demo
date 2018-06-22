using library.Impl.Data.Definition;
using library.Impl.Data.Sql;
using library.Interface.Data.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace library.Impl.Data.Query
{
    public class QueryColumn<A> : IQueryColumn
    {
        public virtual Type Type
        {
            get
            {
                return typeof(A);
            }
        }

        public virtual IQueryRepositoryProperties Query { get; }

        public virtual Description Description { get; }

        public virtual IList<(object value, WhereOperator? sign)> Wheres { get; set; } = new List<(object, WhereOperator?)>();

        public QueryColumn(IQueryRepositoryProperties query, string name, string reference)
        {
            Query = query;

            Description = new Description(name, reference);
        }

        public virtual IQueryRepositoryProperties Where((object value, WhereOperator? sign) condition)
        {
            if (!Wheres.Contains(condition))
                Wheres?.Add(condition);

            return Query;
        }
        public virtual IQueryRepositoryProperties Where(params (object value, WhereOperator? sign)[] conditions)
        {
            conditions?.ToList()?.ForEach(x => Where(x));

            return Query;
        }
        public virtual IQueryRepositoryProperties Where(object value, WhereOperator? sign = WhereOperator.Equals)
        {
            if (value is IList<object>)
                return Where(((IList<object>)value).Select(x => (x, sign)).ToArray());
            else
                return Where((value, sign));
        }
        public virtual IQueryRepositoryProperties Where(IList<object> value, WhereOperator sign = WhereOperator.Equals)
        {
            return Where(value.Select(x => (x, sign)).ToArray());
        }
    }
}
