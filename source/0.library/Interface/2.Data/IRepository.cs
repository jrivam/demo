﻿using library.Impl;
using library.Impl.Data.Sql;
using library.Interface.Data.Model;
using library.Interface.Entities;
using System.Collections.Generic;
using System.Data;

namespace library.Interface.Data
{
    public interface IRepository<T, U> 
        where T : IEntity
        where U : IEntityTable<T>
    {
        (Result result, int rows) ExecuteNonQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null);
        (Result result, int rows) ExecuteNonQuery(IDbCommand command);

        (Result result, object scalar) ExecuteScalar(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null);
        (Result result, object scalar) ExecuteScalar(IDbCommand command);

        (Result result, IEnumerable<U> datas) ExecuteQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null, int maxdepth = 1, IList<U> datas = null);
        (Result result, IEnumerable<U> datas) ExecuteQuery(IDbCommand command, int maxdepth = 1, IList<U> datas = null);
    }
}
