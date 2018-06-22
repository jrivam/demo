using library.Impl.Data.Definition;
using library.Interface.Data.Table;
using System;

namespace library.Impl.Data.Table
{
    public class TableColumn<A> : ITableColumn
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

        public TableColumn(Description tabledescription, string name, string reference)
        {
            TableDescription = tabledescription;

            ColumnDescription = new Description(name, reference);
        }
        public TableColumn(Description tabledescription, string name, string reference, bool isprimarykey)
            : this(tabledescription, name, reference)
        {
            IsPrimaryKey = isprimarykey;
        }
        public TableColumn(Description tabledescription, string name, string reference, bool isprimarykey, bool isidentity)
            : this(tabledescription, name, reference, isprimarykey)
        {
            IsIdentity = isidentity;
        }
    }
}
