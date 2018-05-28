using library.Impl.Data.Sql;
using library.Interface.Data.Model;
using library.Interface.Data.Repository;
using library.Interface.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace library.Impl.Data.Model
{
    public abstract class AbstractEntityTable<T> : IEntityTable<T>
        where T : IEntity, new()
    {
        public virtual T Entity { get; protected set; } = new T();

        public virtual string Name { get; protected set; }
        public virtual string Reference { get; protected set; }

        public AbstractEntityTable(string name, string reference)
        {
            Name = name;
            Reference = reference;
        }

        public virtual bool UseDbCommand { get; set; }
        public virtual (bool usedbcommand, (string text, CommandType type, IList<DbParameter> parameters) dbcommand)? SelectDbCommand { get; set; }
        public virtual (bool usedbcommand, (string text, CommandType type, IList<DbParameter> parameters) dbcommand)? InsertDbCommand { get; set; }
        public virtual (bool usedbcommand, (string text, CommandType type, IList<DbParameter> parameters) dbcommand)? UpdateDbCommand { get; set; }
        public virtual (bool usedbcommand, (string text, CommandType type, IList<DbParameter> parameters) dbcommand)? DeleteDbCommand { get; set; }

        public virtual void InitDbCommands()
        {
        }

        public virtual IEntityColumn<T> this[string reference]
        {
            get
            {
                return Columns.SingleOrDefault(x => x.Reference.ToLower() == reference.ToLower());
            }
        }
        public virtual IList<IEntityColumn<T>> Columns { get; } = new List<IEntityColumn<T>>();
    }
}
