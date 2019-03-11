using library.Impl.Data.Definition;
using library.Impl.Data.Sql;
using library.Interface.Data.Query;
using System.Collections.Generic;

namespace library.Impl.Data.Query
{
    public abstract class AbstractQueryRepository : IQueryRepository
    {
        public virtual Description Description { get; protected set; }

        public AbstractQueryRepository(string name, string reference)
        {
            Description = new Description(name, reference);
        }

        public virtual IList<(IQueryRepository internaltable, IQueryColumn internalkey, IQueryRepository externaltable, IQueryColumn externalkey)> GetJoins(int maxdepth = 1, int depth = 0)
        {
            return new List<(IQueryRepository internaltable, IQueryColumn internalkey, IQueryRepository externaltable, IQueryColumn externalkey)>();
        }

        public virtual IList<(IQueryColumn column, OrderDirection flow)> Orders { get; } = new List<(IQueryColumn, OrderDirection)>();

        public virtual IQueryColumn this[string reference]
        {
            get
            {
                return Columns[reference];
            }
        }
        public virtual ListColumns<IQueryColumn> Columns { get; set; } = new ListColumns<IQueryColumn>();

        public void ClearWhere()
        {
            foreach (var column in Columns)
                column.Wheres.Clear();
        }
    }
}
