using library.Interface.Data.Query;
using library.Interface.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace library.Impl.Data.Repository
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

        public virtual IQueryTable Table { get; }

        public virtual string Name { get; }
        public virtual string Reference { get; }

        public virtual IList<(object value, WhereOperator sign)> Wheres { get; set; } = new List<(object, WhereOperator)>();

        public QueryColumn(IQueryTable table, string name, string reference)
        {
            Table = table;

            Name = name;
            Reference = reference;
        }

        public virtual IQueryTable Where((object value, WhereOperator sign) condition)
        {
            if (!Wheres.Contains(condition))
                Wheres?.Add(condition);

            return Table;
        }
        public virtual IQueryTable Where(params (object value, WhereOperator sign)[] conditions)
        {
            conditions?.ToList()?.ForEach(x => Where(x));

            return Table;
        }
        public virtual IQueryTable Where(object value, WhereOperator sign = WhereOperator.Equals)
        {
            if (value is IList<object>)
                return Where(((IList<object>)value).Select(x => (x, sign)).ToArray());
            else
                return Where((value, sign));
        }
        public virtual IQueryTable Where(IList<object> value, WhereOperator sign = WhereOperator.Equals)
        {
            return Where(value.Select(x => (x, sign)).ToArray());
        }
    }
}
