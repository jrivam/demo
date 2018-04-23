using library.Impl.Data.Sql;
using library.Interface.Data.Model;
using library.Interface.Data.Query;
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

    //public partial class AbstractListEntityTable<S, T, U> : List<U>
    //    where T : IEntity
    //    where U : IEntityTable<T>
    //    where S : IQueryRepository<T, U>
    //{
    //    public virtual IList<T> Entities
    //    {
    //        get
    //        {
    //            var list = new List<T>();
    //            this.ForEach(x => list.Add(x.Entity));
    //            return list;
    //        }
    //    }

    //    public AbstractListEntityTable()
    //    {
    //    }

    //    public virtual List<U> Load(S query, int maxdepth = 1, int top = 0)
    //    {
    //        return Load(query.SelectMultiple(maxdepth, top).datas);
    //    }
    //    public virtual List<U> Load(IEnumerable<U> list)
    //    {
    //        this.AddRange(list);

    //        return this;
    //    }
    //}
}
