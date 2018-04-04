using library.Impl.Data.Sql;
using library.Interface.Data;
using library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace library.Impl.Data.Repository
{
    public class EntityFrameworkRepository<T, U> : IRepository<T, U> where T : IEntity
                                                                     where U : IEntityTable<T>
    {
        public EntityFrameworkRepository()
        {

        }

        U IRepository<T, U>.Clear(U data, int maxdepth)
        {
            throw new NotImplementedException();
        }

        (Result result, int rows) IRepository<T, U>.Delete(IQueryTable querytable, int maxdepth)
        {
            throw new NotImplementedException();
        }

        (Result result, int rows) IRepository<T, U>.Delete(string commandtext, CommandType commandtype, IList<DbParameter> parameters)
        {
            throw new NotImplementedException();
        }

        (Result result, int rows) IRepository<T, U>.Delete(IDbCommand command)
        {
            throw new NotImplementedException();
        }

        (Result result, U data) IRepository<T, U>.Delete(U data, bool usedbcommand)
        {
            throw new NotImplementedException();
        }

        (Result result, U data) IRepository<T, U>.Delete(U data, (string commandtext, CommandType commandtype, IList<DbParameter> parameters) dbcommand)
        {
            throw new NotImplementedException();
        }

        (Result result, U data) IRepository<T, U>.Delete(U data, string commandtext, CommandType commandtype, IList<DbParameter> parameters)
        {
            throw new NotImplementedException();
        }

        (Result result, U data) IRepository<T, U>.Delete(U data, IDbCommand command)
        {
            throw new NotImplementedException();
        }

        (Result result, int rows) IRepository<T, U>.ExecuteNonQuery(string commandtext, CommandType commandtype, IList<DbParameter> parameters)
        {
            throw new NotImplementedException();
        }

        (Result result, int rows) IRepository<T, U>.ExecuteNonQuery(IDbCommand command)
        {
            throw new NotImplementedException();
        }

        (Result result, IEnumerable<U> datas) IRepository<T, U>.ExecuteQuery(string commandtext, CommandType commandtype, IList<DbParameter> parameters, int maxdepth, IList<U> datas)
        {
            throw new NotImplementedException();
        }

        (Result result, IEnumerable<U> datas) IRepository<T, U>.ExecuteQuery(IDbCommand command, int maxdepth, IList<U> datas)
        {
            throw new NotImplementedException();
        }

        (Result result, object scalar) IRepository<T, U>.ExecuteScalar(string commandtext, CommandType commandtype, IList<DbParameter> parameters)
        {
            throw new NotImplementedException();
        }

        (Result result, object scalar) IRepository<T, U>.ExecuteScalar(IDbCommand command)
        {
            throw new NotImplementedException();
        }

        (Result result, U data) IRepository<T, U>.Insert(U data, bool usedbcommand)
        {
            throw new NotImplementedException();
        }

        (Result result, U data) IRepository<T, U>.Insert(U data, (string commandtext, CommandType commandtype, IList<DbParameter> parameters) dbcommand)
        {
            throw new NotImplementedException();
        }

        (Result result, U data) IRepository<T, U>.Insert(U data, string commandtext, CommandType commandtype, IList<DbParameter> parameters)
        {
            throw new NotImplementedException();
        }

        (Result result, U data) IRepository<T, U>.Insert(U data, IDbCommand command)
        {
            throw new NotImplementedException();
        }

        (Result result, U data) IRepository<T, U>.Select(U data, bool usedbcommand)
        {
            throw new NotImplementedException();
        }

        (Result result, U data) IRepository<T, U>.Select(U data, (string commandtext, CommandType commandtype, IList<DbParameter> parameters) dbcommand)
        {
            throw new NotImplementedException();
        }

        (Result result, U data) IRepository<T, U>.Select(U data, string commandtext, CommandType commandtype, IList<DbParameter> parameters)
        {
            throw new NotImplementedException();
        }

        (Result result, U data) IRepository<T, U>.Select(U data, IDbCommand command)
        {
            throw new NotImplementedException();
        }

        (Result result, IEnumerable<U> datas) IRepository<T, U>.SelectMultiple(IQueryTable querytable, int maxdepth, int top, IList<U> datas)
        {
            throw new NotImplementedException();
        }

        (Result result, IEnumerable<U> datas) IRepository<T, U>.SelectMultiple(string commandtext, CommandType commandtype, IList<DbParameter> parameters, int maxdepth, IList<U> datas)
        {
            throw new NotImplementedException();
        }

        (Result result, IEnumerable<U> datas) IRepository<T, U>.SelectMultiple(IDbCommand command, int maxdepth, IList<U> datas)
        {
            throw new NotImplementedException();
        }

        (Result result, U data) IRepository<T, U>.SelectSingle(IQueryTable querytable, int maxdepth, U data)
        {
            throw new NotImplementedException();
        }

        (Result result, U data) IRepository<T, U>.SelectSingle(string commandtext, CommandType commandtype, IList<DbParameter> parameters, int maxdepth, U data)
        {
            throw new NotImplementedException();
        }

        (Result result, U data) IRepository<T, U>.SelectSingle(IDbCommand command, int maxdepth, U data)
        {
            throw new NotImplementedException();
        }

        (Result result, int rows) IRepository<T, U>.Update(U table, IQueryTable querytable, int maxdepth)
        {
            throw new NotImplementedException();
        }

        (Result result, int rows) IRepository<T, U>.Update(string commandtext, CommandType commandtype, IList<DbParameter> parameters)
        {
            throw new NotImplementedException();
        }

        (Result result, int rows) IRepository<T, U>.Update(IDbCommand command)
        {
            throw new NotImplementedException();
        }

        (Result result, U data) IRepository<T, U>.Update(U data, bool usedbcommand)
        {
            throw new NotImplementedException();
        }

        (Result result, U data) IRepository<T, U>.Update(U data, (string commandtext, CommandType commandtype, IList<DbParameter> parameters) dbcommand)
        {
            throw new NotImplementedException();
        }

        (Result result, U data) IRepository<T, U>.Update(U data, string commandtext, CommandType commandtype, IList<DbParameter> parameters)
        {
            throw new NotImplementedException();
        }

        (Result result, U data) IRepository<T, U>.Update(U data, IDbCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
