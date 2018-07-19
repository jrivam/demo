using library.Impl;
using library.Impl.Data.Sql;
using library.Interface.Entities;
using System.Collections.Generic;
using System.Data;

namespace library.Interface.Data.Table
{
    public interface IRepositoryTable<T, U>
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
    {
        (Result result, U data) Select(U data, bool usedbcommand = false);
        (Result result, U data) Select(U data, (string commandtext, CommandType commandtype, IList<SqlParameter> parameters) dbcommand);
        (Result result, U data) Select(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null);
        (Result result, U data) Select(U data, IDbCommand command);

        (Result result, U data) Insert(U data, bool usedbcommand = false);
        (Result result, U data) Insert(U data, (string commandtext, CommandType commandtype, IList<SqlParameter> parameters) dbcommand);
        (Result result, U data) Insert(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null);
        (Result result, U data) Insert(U data, IDbCommand command);

        (Result result, U data) Update(U data, bool usedbcommand = false);
        (Result result, U data) Update(U data, (string commandtext, CommandType commandtype, IList<SqlParameter> parameters) dbcommand);
        (Result result, U data) Update(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null);
        (Result result, U data) Update(U data, IDbCommand command);

        (Result result, U data) Delete(U data, bool usedbcommand = false);
        (Result result, U data) Delete(U data, (string commandtext, CommandType commandtype, IList<SqlParameter> parameters) dbcommand);
        (Result result, U data) Delete(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null);
        (Result result, U data) Delete(U data, IDbCommand command);
    }
}
