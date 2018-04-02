using library.Impl.Data.Sql;
using library.Interface.Data.Repository;
using library.Interface.Entities;
using System.Collections.Generic;
using System.Data;

namespace library.Interface.Data
{
    public interface IEntityTable<T> where T : IEntity
    {
        T Entity { get; }

        string Reference { get; }
        string Name { get; }

        (string text, CommandType type, IList<DbParameter> parameters)? SelectDbCommand { get; set; }
        (string text, CommandType type, IList<DbParameter> parameters)? InsertDbCommand { get; set; }
        (string text, CommandType type, IList<DbParameter> parameters)? UpdateDbCommand { get; set; }
        (string text, CommandType type, IList<DbParameter> parameters)? DeleteDbCommand { get; set; }

        IList<IEntityColumn<T>> Columns { get; }
        IEntityColumn<T> this[string reference] { get; }
    }
}
