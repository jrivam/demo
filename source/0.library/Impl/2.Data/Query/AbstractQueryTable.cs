using library.Interface.Data.Query;
using library.Interface.Data.Repository;
using System.Collections.Generic;
using System.Linq;

namespace library.Impl.Data.Query
{
    public abstract class AbstractQueryTable : IQueryTable
    {
        public virtual string Name { get; protected set; }
        public virtual string Reference { get; protected set; }

        public AbstractQueryTable(string name, string reference)
        {
            Name = name;
            Reference = reference;
        }

        public virtual IList<(IQueryColumn column, OrderDirection flow)> Orders { get; } = new List<(IQueryColumn, OrderDirection)>();

        public virtual IQueryColumn this[string reference]
        {
            get
            {
                return Columns.SingleOrDefault(x => x.Reference.ToLower() == reference.ToLower());
            }
        }
        public virtual IList<IQueryColumn> Columns { get; } = new List<IQueryColumn>();

        public virtual IList<(IQueryColumn internalkey, IQueryColumn externalkey)> Joins { get; } = new List<(IQueryColumn, IQueryColumn)>();
    }
}
