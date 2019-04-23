using Library.Interface.Persistence.Table;

namespace Library.Impl.Persistence.Table
{
    public class ColumnTable<A> : Column<A>, IColumnTable
    {
        public virtual IBuilderTableData Table { get; protected set; }

        public virtual bool IsPrimaryKey { get; }
        public virtual bool IsIdentity { get; }
        public virtual bool IsUnique { get; }

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

        public ColumnTable(IBuilderTableData table, 
            string name, string reference)
            : base(name, reference)
        {
            Table = table;
        }
        public ColumnTable(IBuilderTableData table, 
            string name, string reference, bool isprimarykey = false)
            : this(table,
                  name, reference)
        {
            IsPrimaryKey = isprimarykey;
        }
        public ColumnTable(IBuilderTableData table, 
            string name, string reference, bool isprimarykey = false, bool isidentity = false)
            : this(table,
                  name, reference, isprimarykey)
        {
            IsIdentity = isidentity;
        }
        public ColumnTable(IBuilderTableData table, 
            string name, string reference, bool isprimarykey = false, bool isidentity = false, bool isunique = false)
            : this(table,
                  name, reference, isprimarykey, isidentity)
        {
            IsUnique = isunique;
        }
    }
}
