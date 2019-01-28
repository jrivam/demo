using library.Impl.Data.Definition;
using library.Impl.Data.Sql;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace library.Impl.Data.Table
{
    public abstract class AbstractTableRepository<T> : ITableRepository, ITableEntity<T>
        where T : IEntity, new()
    {
        public virtual T Entity { get; protected set; }

        public virtual Description Description { get; protected set; }

        public AbstractTableRepository(T entity,
            string name, string reference)
        {
            Entity = entity;

            Description = new Description(name, reference);
        }

        public virtual bool UseDbCommand { get; set; }
        public virtual (bool usedbcommand, (string text, CommandType type, IList<SqlParameter> parameters) dbcommand)? SelectDbCommand { get; set; }
        public virtual (bool usedbcommand, (string text, CommandType type, IList<SqlParameter> parameters) dbcommand)? InsertDbCommand { get; set; }
        public virtual (bool usedbcommand, (string text, CommandType type, IList<SqlParameter> parameters) dbcommand)? UpdateDbCommand { get; set; }
        public virtual (bool usedbcommand, (string text, CommandType type, IList<SqlParameter> parameters) dbcommand)? DeleteDbCommand { get; set; }

        public virtual void InitDbCommands()
        {
        }

        public virtual ITableColumn this[string reference]
        {
            get
            {
                return Columns.SingleOrDefault(x => x.Description.Reference.ToLower() == reference.ToLower());
            }
        }
        public virtual IList<ITableColumn> Columns { get; } = new List<ITableColumn>();
    }
}
