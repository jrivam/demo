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

        bool UseDbCommand { get; set; }
        (bool usedbcommand, (string text, CommandType type, IList<DbParameter> parameters) dbcommand)? SelectDbCommand { get; set; }
        (bool usedbcommand, (string text, CommandType type, IList<DbParameter> parameters) dbcommand)? InsertDbCommand { get; set; }
        (bool usedbcommand, (string text, CommandType type, IList<DbParameter> parameters) dbcommand)? UpdateDbCommand { get; set; }
        (bool usedbcommand, (string text, CommandType type, IList<DbParameter> parameters) dbcommand)? DeleteDbCommand { get; set; }

        IList<IEntityColumn<T>> Columns { get; }
        IEntityColumn<T> this[string reference] { get; }

        void InitDbCommands();
    }
}
