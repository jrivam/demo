using Library.Interface.Persistence.Table;

namespace Library.Impl.Persistence.Table
{
    public class ColumnTable<A> : Column<A>, IColumnTable
    {
        public virtual IBuilderTableData Table { get; protected set; }

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

        public virtual bool IsPrimaryKey { get; }
        public virtual bool IsIdentity { get; }
        public virtual bool IsForeignKey { get; }

        public ColumnTable(IBuilderTableData table,
            string name, string dbname)
            : base(name, dbname)
        {
            Table = table;
        }

        public ColumnTable(IBuilderTableData table,
            string name, string dbname, 
            bool isprimarykey = false, bool isidentity = false,
            bool isforeignkey = false)
            : this(table,
                name, dbname)
        {
            IsPrimaryKey = isprimarykey;
            IsIdentity = isidentity;
            IsForeignKey = isforeignkey;
        }
    }
}
