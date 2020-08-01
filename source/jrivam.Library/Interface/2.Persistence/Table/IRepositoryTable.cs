using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Sql;
using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Interface.Persistence.Table
{
    public interface IRepositoryTable<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        (Result result, U data) Select(U data);
        (Result result, U data) Select(U data, ISqlCommand dbcommand);
        (Result result, U data) Select(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, ISqlParameter[] parameters = null);

        (Result result, U data) Insert(U data);
        (Result result, U data) Insert(U data, ISqlCommand dbcommand);
        (Result result, U data) Insert(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, ISqlParameter[] parameters = null);

        (Result result, U data) Update(U data);
        (Result result, U data) Update(U data, ISqlCommand dbcommand);
        (Result result, U data) Update(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, ISqlParameter[] parameters = null);

        (Result result, U data) Delete(U data);
        (Result result, U data) Delete(U data, ISqlCommand dbcommand);
        (Result result, U data) Delete(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, ISqlParameter[] parameters = null);
    }
}
