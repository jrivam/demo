﻿using jrivam.Library.Impl;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using System.Data;
using System.Threading.Tasks;

namespace jrivam.Library.Interface.Business.Table
{
    public interface ILogicTableAsync<T, U, V>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
    {
        Task<(Result result, V domain)> LoadQueryAsync(V domain, int maxdepth = 1, IDbConnection connection = null, int? commandtimeout = null);

        Task<(Result result, V domain)> LoadAsync(V domain, IDbConnection connection = null, int? commandtimeout = null);

        Task<(Result result, V domain)> SaveAsync(V domain, IDbConnection connection = null, IDbTransaction transaction = null, int? commandtimeout = null);

        Task<(Result result, V domain)> EraseAsync(V domain, IDbConnection connection = null, IDbTransaction transaction = null, int? commandtimeout = null);
    }
}
