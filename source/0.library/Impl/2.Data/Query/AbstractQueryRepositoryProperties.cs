﻿using library.Impl.Data.Repository;
using library.Interface.Data;
using library.Interface.Data.Query;
using System.Collections.Generic;
using System.Linq;

namespace library.Impl.Data.Query
{
    public abstract class AbstractQueryRepositoryProperties : IQueryRepositoryProperties
    {
        public virtual Description Description { get; protected set; }
        public AbstractQueryRepositoryProperties(string name, string reference)
        {
            Description = new Description(name, reference);
        }

        public virtual IList<(IQueryColumn column, OrderDirection flow)> Orders { get; } = new List<(IQueryColumn, OrderDirection)>();

        public virtual IQueryColumn this[string reference]
        {
            get
            {
                return Columns.SingleOrDefault(x => x.Description.Reference.ToLower() == reference.ToLower());
            }
        }
        public virtual IList<IQueryColumn> Columns { get; } = new List<IQueryColumn>();

        public virtual IList<(IQueryColumn internalkey, IQueryColumn externalkey)> Joins { get; } = new List<(IQueryColumn, IQueryColumn)>();

        public void ClearWhere()
        {
            foreach (var column in Columns)
                column.Wheres.Clear();
        }
    }
}