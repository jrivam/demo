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

        public virtual string RequiredMessage { get; set; }

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
            string name, string reference,
            string requiredmessage = "")
            : this(table,
                  name, reference)
        {
            RequiredMessage = requiredmessage;
        }

        public ColumnTable(IBuilderTableData table,
            string name, string reference,
            string requiredmessage = "",
            bool isprimarykey = false, bool isidentity = false, bool isunique = false)
            : this(table,
                name, reference,
                requiredmessage)
        {
            IsPrimaryKey = isprimarykey;
            IsIdentity = isidentity;
            IsUnique = isunique;
        }


    }
}
