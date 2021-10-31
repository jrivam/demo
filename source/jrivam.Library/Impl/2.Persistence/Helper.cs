using jrivam.Library.Impl.Persistence.Sql;
using MySql.Data.MySqlClient;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace jrivam.Library.Impl.Persistence
{
    public class Helper
    {    
        public static IDictionary<Type, DbType> TypeToDbType
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

        public static IDictionary<DbType, SqlDbType> DbTypeToSqlServerType
        {
            get
            {
                return new Dictionary<DbType, SqlDbType>()
                {
                    { DbType.String, SqlDbType.NVarChar },
                    { DbType.Guid, SqlDbType.UniqueIdentifier },
                    { DbType.Boolean, SqlDbType.Bit },
                    { DbType.DateTime, SqlDbType.DateTime },
                    { DbType.Int16, SqlDbType.SmallInt },
                    { DbType.Int32, SqlDbType.Int },
                    { DbType.Int64, SqlDbType.BigInt },
                    { DbType.Decimal, SqlDbType.Decimal },
                    { DbType.Double, SqlDbType.Float },
                    { DbType.Single, SqlDbType.Real },
                    { DbType.Byte, SqlDbType.TinyInt },
                    { DbType.Binary, SqlDbType.VarBinary },
                    { DbType.Object, SqlDbType.Variant },
                    { DbType.DateTimeOffset, SqlDbType.DateTimeOffset },
                    { DbType.Time, SqlDbType.Time },
                };
            }
        }

        public static IDictionary<DbType, MySqlDbType> DbTypeToMySqlType
        {
            get
            {
                return new Dictionary<DbType, MySqlDbType>()
                {
                    { DbType.String, MySqlDbType.VarChar },
                    { DbType.Guid, MySqlDbType.Guid },
                    { DbType.Boolean, MySqlDbType.Bit },
                    { DbType.DateTime, MySqlDbType.DateTime },
                    { DbType.Int16, MySqlDbType.Int16 },
                    { DbType.Int32, MySqlDbType.Int32 },
                    { DbType.Int64, MySqlDbType.Int64 },
                    { DbType.Decimal, MySqlDbType.Decimal },
                    { DbType.Double, MySqlDbType.Double },
                    { DbType.Single, MySqlDbType.Float },
                    { DbType.Byte, MySqlDbType.Byte },
                    { DbType.Binary, MySqlDbType.VarBinary },
                    { DbType.Object, MySqlDbType.Blob },
                    { DbType.DateTimeOffset, MySqlDbType.DateTime },
                    { DbType.Time, MySqlDbType.Time },
                };
            }
        }

        public static IDictionary<DbType, NpgsqlDbType> DbTypeToPostgreSqlType
        {
            get
            {
                return new Dictionary<DbType, NpgsqlDbType>()
                {
                    { DbType.String, NpgsqlDbType.Text },
                    { DbType.StringFixedLength, NpgsqlDbType.Text },
                    { DbType.AnsiString, NpgsqlDbType.Text },
                    { DbType.AnsiStringFixedLength, NpgsqlDbType.Text },
                    { DbType.Boolean, NpgsqlDbType.Boolean },
                    { DbType.Date, NpgsqlDbType.Date },
                    { DbType.DateTime, NpgsqlDbType.Timestamp },
                    { DbType.DateTime2, NpgsqlDbType.Timestamp },
                    { DbType.DateTimeOffset, NpgsqlDbType.TimestampTZ },
                    { DbType.Int16, NpgsqlDbType.Smallint },
                    { DbType.Int32, NpgsqlDbType.Integer},
                    { DbType.Int64, NpgsqlDbType.Bigint },
                    { DbType.Decimal, NpgsqlDbType.Numeric },
                    { DbType.VarNumeric, NpgsqlDbType.Numeric },
                    { DbType.Currency, NpgsqlDbType.Money },
                    { DbType.Double, NpgsqlDbType.Double },
                    { DbType.Single, NpgsqlDbType.Real },
                    { DbType.Binary, NpgsqlDbType.Bytea },
                    { DbType.Time, NpgsqlDbType.Time },
                };
            }
        }

        public static bool UseDbCommand(bool classusedbcommand, bool propertyusedbcommand, bool methodusedbcommand)
        {
            return (methodusedbcommand || propertyusedbcommand || classusedbcommand || Convert.ToBoolean(ConfigurationManager.AppSettings["database.forceusedbcommand"]));
        }

        public static int CommandTimeout(int? methodcommandtimeout)
        {
            return (methodcommandtimeout ?? (int.TryParse(ConfigurationManager.AppSettings["database.commandtimeout"], out var tryparseintvalue) ? tryparseintvalue : 30));
        }

        public static SqlParameter
            GetParameter
            (string name, Type type, object value, ParameterDirection direction = ParameterDirection.Input)
        {
            return new SqlParameter()
            {
                Name = name,
                Type = type,
                Value = value,

                Direction = direction
            };
        }
    }
}
