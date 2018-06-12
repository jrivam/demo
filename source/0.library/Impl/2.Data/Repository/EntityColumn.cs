using library.Interface.Data;
using System;

namespace library.Impl.Data.Repository
{
    public class EntityColumn<A> : IEntityColumn
    {
        public virtual Type Type
        {
            get
            {
                return typeof(A);
            }
        }

        public virtual Description ColumnDescription { get; }
        public virtual Description TableDescription { get; }

        public virtual bool IsPrimaryKey { get; }
        public virtual bool IsIdentity { get; }

        public virtual object Value { get; set; }
        public virtual object DbValue { get; set; }

        public EntityColumn(Description tabledescription, string name, string reference)
        {
            TableDescription = tabledescription;

            ColumnDescription = new Description(name, reference);
        }
        public EntityColumn(Description tabledescription, string name, string reference, bool isprimarykey)
            : this(tabledescription, name, reference)
        {
            IsPrimaryKey = isprimarykey;
        }
        public EntityColumn(Description tabledescription, string name, string reference, bool isprimarykey, bool isidentity)
            : this(tabledescription, name, reference, isprimarykey)
        {
            IsIdentity = isidentity;
        }
    }
}
