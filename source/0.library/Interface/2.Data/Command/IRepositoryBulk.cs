﻿using library.Impl;
using library.Impl.Data.Sql;
using System.Collections.Generic;
using System.Data;

namespace library.Interface.Data.Command
{
    public interface IRepositoryBulk
    {
        (Result result, int rows) ExecuteNonQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null);
        (Result result, object scalar) ExecuteScalar(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null);
    }
}