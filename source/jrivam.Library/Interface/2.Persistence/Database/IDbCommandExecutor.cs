﻿using jrivam.Library.Impl;
using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Interface.Persistence.Database
{
    public interface IDbCommandExecutor<T>
    {
        (Result result, IEnumerable<T> entities) ExecuteQuery(IDbCommand command, int maxdepth = 1, ICollection<T> entities = null);
    }
}