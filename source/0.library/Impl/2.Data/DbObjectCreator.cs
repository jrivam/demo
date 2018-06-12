﻿using library.Interface.Data.Sql;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace library.Impl.Data
{
    public class DbObjectCreator : IDbObjectCreator
    {
        private readonly ConnectionStringSettings _connectionstringsettings;

        public DbObjectCreator(ConnectionStringSettings connectionstringsettings)
        {
            _connectionstringsettings = connectionstringsettings;
        }

        public virtual DbProviderFactory ProviderFactory
        {
            get
            {
                return DbProviderFactories.GetFactory(_connectionstringsettings.ProviderName);
            }
        }
        public virtual IDbConnection Connection
        {
            get
            {
                var connection = ProviderFactory.CreateConnection();
                connection.ConnectionString = _connectionstringsettings.ConnectionString;
                return connection;
            }
        }
        public virtual IDbCommand Command
        {
            get
            {
                return ProviderFactory.CreateCommand();
            }
        }
        public virtual DbParameter Parameter
        {
            get
            {
                return ProviderFactory.CreateParameter();
            }
        }
        public virtual DbDataAdapter Adapter
        {
            get
            {
                return ProviderFactory.CreateDataAdapter();
            }
        }
    }
}
