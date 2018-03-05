using library.Interface.Data.Sql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace library.Impl.Data
{
    public class DbObjectCreator : IDbObjectCreator
    {
        public virtual IDictionary<Type, DbType> TypeToDbType
        {
            get
            {
                return new Dictionary<Type, DbType>()
                {
                    { typeof(string), DbType.String },
                    { typeof(char), DbType.String },
                    { typeof(char[]), DbType.String },
                    { typeof(Guid?), DbType.Guid },
                    { typeof(bool?), DbType.Boolean },
                    { typeof(DateTime?), DbType.DateTime },
                    { typeof(short?), DbType.Int16 },
                    { typeof(int?), DbType.Int32 },
                    { typeof(long?), DbType.Int64 },
                    { typeof(decimal?), DbType.Decimal },
                    { typeof(double?), DbType.Double },
                    { typeof(Single?), DbType.Single },
                    { typeof(byte?), DbType.Byte },
                    { typeof(byte[]), DbType.Binary },
                    { typeof(object), DbType.Object },
                    { typeof(DateTimeOffset?), DbType.DateTimeOffset },
                    { typeof(TimeSpan?), DbType.Time },
                };
            }
        }

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
