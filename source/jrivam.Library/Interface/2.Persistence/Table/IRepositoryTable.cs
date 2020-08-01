using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Sql;
using System.Data;

namespace jrivam.Library.Interface.Persistence.Table
{
    public interface IRepositoryTable<T, U>
        where T : IEntity
        where U : ITableData<T, U>
    {
        (Result result, U data) Select(U data, IDbConnection connection = null);
        (Result result, U data) Select(U data, ISqlCommand dbcommand, IDbConnection connection = null);
        (Result result, U data) Select(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, ISqlParameter[] parameters = null, IDbConnection connection = null);

        (Result result, U data) Insert(U data, IDbConnection connection = null, IDbTransaction transaction = null);
        (Result result, U data) Insert(U data, ISqlCommand dbcommand, IDbConnection connection = null, IDbTransaction transaction = null);
        (Result result, U data) Insert(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, ISqlParameter[] parameters = null, IDbConnection connection = null, IDbTransaction transaction = null);

        (Result result, U data) Update(U data, IDbConnection connection = null, IDbTransaction transaction = null);
        (Result result, U data) Update(U data, ISqlCommand dbcommand, IDbConnection connection = null, IDbTransaction transaction = null);
        (Result result, U data) Update(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, ISqlParameter[] parameters = null, IDbConnection connection = null, IDbTransaction transaction = null);

        (Result result, U data) Delete(U data, IDbConnection connection = null, IDbTransaction transaction = null);
        (Result result, U data) Delete(U data, ISqlCommand dbcommand, IDbConnection connection = null, IDbTransaction transaction = null);
        (Result result, U data) Delete(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, ISqlParameter[] parameters = null, IDbConnection connection = null, IDbTransaction transaction = null);
    }
}
