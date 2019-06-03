using Library.Impl;
using Library.Impl.Persistence.Sql;
using Library.Interface.Entities;
using Library.Interface.Persistence.Sql;
using System.Collections.Generic;
using System.Data;

namespace Library.Interface.Persistence.Table
{
    public interface IRepositoryTable<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        (Result result, U data) Select(U table, bool usedbcommand = false);
        (Result result, U data) Select(U table, ISqlCommand dbcommand);
        (Result result, U data) Select(U table, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null);

        (Result result, U data) Insert(U table, bool usedbcommand = false);
        (Result result, U data) Insert(U table, ISqlCommand dbcommand);
        (Result result, U data) Insert(U table, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null);

        (Result result, U data) Update(U table, bool usedbcommand = false);
        (Result result, U data) Update(U table, ISqlCommand dbcommand);
        (Result result, U data) Update(U table, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null);

        (Result result, U data) Delete(U table, bool usedbcommand = false);
        (Result result, U data) Delete(U table, ISqlCommand dbcommand);
        (Result result, U data) Delete(U table, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null);
    }
}
