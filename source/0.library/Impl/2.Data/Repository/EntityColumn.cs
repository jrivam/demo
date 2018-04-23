using library.Interface.Data.Model;
using library.Interface.Data.Repository;
using library.Interface.Entities;
using System;

namespace library.Impl.Data.Repository
{
    public class EntityColumn<A, T> : IEntityColumn<T> 
        where T : IEntity
    {
        public virtual Type Type
        {
            get
            {
                return typeof(A);
            }
        }

        public IEntityTable<T> Table { get; }

        public virtual string Name { get; }
        public virtual string Reference { get; }

        public virtual bool IsPrimaryKey { get; }
        public virtual bool IsIdentity { get; }

        public virtual object Value { get; set; }
        public virtual object DbValue { get; set; }

        public EntityColumn(IEntityTable<T> table, string name, string reference)
        {
            Table = table;

            Name = name;
            Reference = reference;
        }
        public EntityColumn(IEntityTable<T> table, string name, string reference, bool isprimarykey)
            : this(table, name, reference)
        {
            IsPrimaryKey = isprimarykey;
        }
        public EntityColumn(IEntityTable<T> table, string name, string reference, bool isprimarykey, bool isidentity)
            : this(table, name, reference, isprimarykey)
        {
            IsIdentity = isidentity;
        }
    }
}
