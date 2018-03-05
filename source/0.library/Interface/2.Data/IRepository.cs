using library.Impl;
using library.Impl.Data.Sql;
using library.Interface.Domain;
using System.Collections.Generic;
using System.Data;

namespace library.Interface.Data
{
    public interface IRepository<T, U> where T : IEntity
                                       where U : IEntityTable<T>
    {
        U Clear(U data, int maxdepth = 1);

        (Result result, U data) SelectSingle(IQueryTable query, int maxdepth = 1);
        (Result result, U data) SelectSingle(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null, int maxdepth = 1);
        (Result result, U data) SelectSingle(IDbCommand command, int maxdepth = 1);

        (Result result, IEnumerable<U> datas) SelectMultiple(IQueryTable query, int maxdepth = 1, int top = 0);
        (Result result, IEnumerable<U> datas) SelectMultiple(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null, int maxdepth = 1);
        (Result result, IEnumerable<U> datas) SelectMultiple(IDbCommand command, int maxdepth = 1);

        (Result result, int rows) Update(U table, IQueryTable query, int maxdepth = 1);
        (Result result, int rows) Update(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null);
        (Result result, int rows) Update(IDbCommand command);

        (Result result, int rows) Delete(IQueryTable query, int maxdepth = 1);
        (Result result, int rows) Delete(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null);
        (Result result, int rows) Delete(IDbCommand command);

        (Result result, U data) Select(U data, int maxdepth = 1);
        (Result result, U data) Select(U data, (string commandtext, CommandType commandtype, IList<DbParameter> parameters) entitycommand, int maxdepth = 1);
        (Result result, U data) Select(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<DbParameter> parameters = null, int maxdepth = 1);
        (Result result, U data) Select(U data, IDbCommand command, int maxdepth = 1);

        (Result result, U data) Insert(U data);
        (Result result, U data) Insert(U data, (string commandtext, CommandType commandtype, IList<DbParameter> parameters) entitycommand);
        (Result result, U data) Insert(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<DbParameter> parameters = null);
        (Result result, U data) Insert(U data, IDbCommand command);

        (Result result, U data) Update(U data);
        (Result result, U data) Update(U data, (string commandtext, CommandType commandtype, IList<DbParameter> parameters) entitycommand);
        (Result result, U data) Update(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<DbParameter> parameters = null);
        (Result result, U data) Update(U data, IDbCommand command);

        (Result result, U data) Delete(U data);
        (Result result, U data) Delete(U data, (string commandtext, CommandType commandtype, IList<DbParameter> parameters) entitycommand);
        (Result result, U data) Delete(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<DbParameter> parameters = null);
        (Result result, U data) Delete(U data, IDbCommand command);

        (Result result, int rows) ExecuteNonQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null);
        (Result result, int rows) ExecuteNonQuery(IDbCommand command);

        (Result result, object scalar) ExecuteScalar(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null);
        (Result result, object scalar) ExecuteScalar(IDbCommand command);

        (Result result, IEnumerable<U> datas) ExecuteQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null, int maxdepth = 1, IList<U> datas = null);
        (Result result, IEnumerable<U> datas) ExecuteQuery(IDbCommand command, int maxdepth = 1, IList<U> datas = null);
    }
}
