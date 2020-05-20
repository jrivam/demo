using Library.Interface.Persistence;
using System;

namespace Library.Impl.Persistence
{
    public class Column<A> : IColumn
    {
        public virtual Type Type
        {
            get
            {
                return typeof(A);
            }
        }

        public virtual Description Description { get; }

        public Column(string name, string dbname)
        {
            Description = new Description(name, dbname);
        }
    }
}
