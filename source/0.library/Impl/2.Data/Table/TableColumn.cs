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

        public virtual Description Description { get; }

        public virtual bool IsPrimaryKey { get; }
        public virtual bool IsIdentity { get; }

        public virtual object Value { get; set; }

        protected object _dbvalue;
        public virtual object DbValue
        {
            get
            {
                return _dbvalue;
            }
            set
            {
                _dbvalue = value;
                this.Value = _dbvalue;
            }
        }

        public TableColumn(string name, string reference)
        {
            Description = new Description(name, reference);
        }
        public TableColumn(string name, string reference, bool isprimarykey)
            : this(name, reference)
        {
            IsPrimaryKey = isprimarykey;
        }
        public TableColumn(string name, string reference, bool isprimarykey, bool isidentity)
            : this(name, reference, isprimarykey)
        {
            IsIdentity = isidentity;
        }
    }
}
