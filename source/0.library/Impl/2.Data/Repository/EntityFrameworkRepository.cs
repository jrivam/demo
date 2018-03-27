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

        public U Clear(U data, int maxdepth = 1)
        {
            throw new NotImplementedException();
        }

        public (Result result, int rows) Delete(IQueryTable query, int maxdepth = 1)
        {
            throw new NotImplementedException();
        }
        public (Result result, int rows) Delete(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null)
        {
            throw new NotImplementedException();
        }
        public (Result result, int rows) Delete(IDbCommand command)
        {
            throw new NotImplementedException();
        }

        public (Result result, U data) Delete(U data)
        {
            throw new NotImplementedException();
        }
        public (Result result, U data) Delete(U data, (string commandtext, CommandType commandtype, IList<DbParameter> parameters) entitycommand)
        {
            throw new NotImplementedException();
        }
        public (Result result, U data) Delete(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<DbParameter> parameters = null)
        {
            throw new NotImplementedException();
        }

        public (Result result, U data) Delete(U data, IDbCommand command)
        {
            throw new NotImplementedException();
        }
        public (Result result, int rows) ExecuteNonQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null)
        {
            throw new NotImplementedException();
        }
        public (Result result, int rows) ExecuteNonQuery(IDbCommand command)
        {
            throw new NotImplementedException();
        }

        public (Result result, IEnumerable<U> datas) ExecuteQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null, int maxdepth = 1, IList<U> datas = null)
        {
            throw new NotImplementedException();
        }
        public (Result result, IEnumerable<U> datas) ExecuteQuery(IDbCommand command, int maxdepth = 1, IList<U> datas = null)
        {
            throw new NotImplementedException();
        }

        public (Result result, object scalar) ExecuteScalar(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null)
        {
            throw new NotImplementedException();
        }
        public (Result result, object scalar) ExecuteScalar(IDbCommand command)
        {
            throw new NotImplementedException();
        }

        public (Result result, U data) Insert(U data)
        {
            throw new NotImplementedException();
        }
        public (Result result, U data) Insert(U data, (string commandtext, CommandType commandtype, IList<DbParameter> parameters) entitycommand)
        {
            throw new NotImplementedException();
        }
        public (Result result, U data) Insert(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<DbParameter> parameters = null)
        {
            throw new NotImplementedException();
        }
        public (Result result, U data) Insert(U data, IDbCommand command)
        {
            throw new NotImplementedException();
        }

        public (Result result, U data) Select(U data)
        {
            throw new NotImplementedException();
        }
        public (Result result, U data) Select(U data, (string commandtext, CommandType commandtype, IList<DbParameter> parameters) entitycommand)
        {
            throw new NotImplementedException();
        }
        public (Result result, U data) Select(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<DbParameter> parameters = null)
        {
            throw new NotImplementedException();
        }
        public (Result result, U data) Select(U data, IDbCommand command)
        {
            throw new NotImplementedException();
        }

        public (Result result, IEnumerable<U> datas) SelectMultiple(IQueryTable query, int maxdepth = 1, int top = 0, IList<U> datas = null)
        {
            throw new NotImplementedException();
        }
        public (Result result, IEnumerable<U> datas) SelectMultiple(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null, int maxdepth = 1, IList<U> datas = null)
        {
            throw new NotImplementedException();
        } 
        public (Result result, IEnumerable<U> datas) SelectMultiple(IDbCommand command, int maxdepth = 1, IList<U> datas = null)
        {
            throw new NotImplementedException();
        }

        public (Result result, U data) SelectSingle(IQueryTable query, int maxdepth = 1, U data = default(U))
        {
            throw new NotImplementedException();
        }
        public (Result result, U data) SelectSingle(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null, int maxdepth = 1, U data = default(U))
        {
            throw new NotImplementedException();
        }
        public (Result result, U data) SelectSingle(IDbCommand command, int maxdepth = 1, U data = default(U))
        {
            throw new NotImplementedException();
        }

        public (Result result, int rows) Update(U table, IQueryTable query, int maxdepth = 1)
        {
            throw new NotImplementedException();
        }
        public (Result result, int rows) Update(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null)
        {
            throw new NotImplementedException();
        }
        public (Result result, int rows) Update(IDbCommand command)
        {
            throw new NotImplementedException();
        }

        public (Result result, U data) Update(U data)
        {
            throw new NotImplementedException();
        }
        public (Result result, U data) Update(U data, (string commandtext, CommandType commandtype, IList<DbParameter> parameters) entitycommand)
        {
            throw new NotImplementedException();
        }
        public (Result result, U data) Update(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<DbParameter> parameters = null)
        {
            throw new NotImplementedException();
        }
        public (Result result, U data) Update(U data, IDbCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
