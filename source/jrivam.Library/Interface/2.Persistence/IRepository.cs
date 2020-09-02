﻿using jrivam.Library.Impl;
using jrivam.Library.Interface.Persistence.Sql;
using System;
using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Interface.Persistence
{
    public interface IRepository
    {
        (Result result, IEnumerable<T> entities) ExecuteQuery<T>(ISqlCommand sqlcommand, int maxdepth = 1, IDbConnection connection = null);
        (Result result, IEnumerable<T> entities) ExecuteQuery<T>(string commandtext, CommandType commandtype = CommandType.Text, int commandtimeout = 30, ISqlParameter[] parameters = null, int maxdepth = 1, IDbConnection connection = null);

        (Result result, int rows) ExecuteNonQuery(ISqlCommand sqlcommand, IDbConnection connection = null, IDbTransaction transaction = null);
        (Result result, int rows) ExecuteNonQuery(string commandtext, CommandType commandtype = CommandType.Text, int commandtimeout = 30, ISqlParameter[] parameters = null, IDbConnection connection = null, IDbTransaction transaction = null);

        (Result result, T scalar) ExecuteScalar<T>(ISqlCommand sqlcommand, IDbConnection connection = null, IDbTransaction transaction = null);
        (Result result, T scalar) ExecuteScalar<T>(string commandtext, CommandType commandtype = CommandType.Text, int commandtimeout = 30, ISqlParameter[] parameters = null, IDbConnection connection = null, IDbTransaction transaction = null);
    }
}
