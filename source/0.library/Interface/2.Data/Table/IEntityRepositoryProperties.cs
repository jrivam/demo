using library.Impl.Data.Repository;
using library.Impl.Data.Sql;
using library.Interface.Entities;
using System.Collections.Generic;
using System.Data;

namespace library.Interface.Data.Table
{
    public interface IEntityRepositoryProperties<T> 
        where T : IEntity
    {
        T Entity { get; }

        Description Description { get; }

        bool UseDbCommand { get; set; }
        (bool usedbcommand, (string text, CommandType type, IList<SqlParameter> parameters) dbcommand)? SelectDbCommand { get; set; }
        (bool usedbcommand, (string text, CommandType type, IList<SqlParameter> parameters) dbcommand)? InsertDbCommand { get; set; }
        (bool usedbcommand, (string text, CommandType type, IList<SqlParameter> parameters) dbcommand)? UpdateDbCommand { get; set; }
        (bool usedbcommand, (string text, CommandType type, IList<SqlParameter> parameters) dbcommand)? DeleteDbCommand { get; set; }

        IList<IEntityColumn> Columns { get; }
        IEntityColumn this[string reference] { get; }

        void InitDbCommands();
    }
}
