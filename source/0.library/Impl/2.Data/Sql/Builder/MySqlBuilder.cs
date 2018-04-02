using library.Interface.Data;
using library.Interface.Data.Sql;
using library.Interface.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace library.Impl.Data.Sql.Builder
{
    public class MySqlBuilder<T> : AbstractSqlBuilder<T> where T : IEntity
    {
        protected override IDictionary<DbType, object> DbTypeToSqlType
        {
            get
            {
                return new Dictionary<DbType, object>()
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

        public MySqlBuilder(ConnectionStringSettings connectionstringsettings, ISqlSyntaxSign syntaxsign)
            : base(connectionstringsettings, syntaxsign)
        {

        }

        public override IDbCommand Select(IQueryTable querytable, int maxdepth = 1, int top = 0)
        {
            var command = GetCommand();

            var querycolumns = GetQueryColumns(querytable, null, null, maxdepth, 0);
            var queryjoins = GetQueryJoins(querytable, querytable.Name, maxdepth, 0);

            var columns = GetQuerySelectColumns(querycolumns, command);

            var from = GetQueryFrom(queryjoins, querytable.Name);

            var where = GetQueryWhere(querycolumns, command);

            command.CommandText = $"select{Environment.NewLine}{columns}from {from}{where}";
            command.CommandText += $"{(top > 0 ? $" limit {top.ToString()}" : "")}";

            return command;
        }

        public override IDbCommand Update(IEntityTable<T> entitytable, IQueryTable querytable, int maxdepth = 1)
        {
            var command = GetCommand();

            var querycolumns = GetQueryColumns(querytable, null, null, maxdepth, 0);
            var queryjoins = GetQueryJoins(querytable, querytable.Name, maxdepth, 0);

            var from = GetQueryFrom(queryjoins, entitytable.Name);

            var set = GetEntityUpdateSet(entitytable.Columns.Where(c => !c.IsIdentity && c.Value != c.DbValue).ToList(), command);

            var where = GetQueryWhere(querycolumns, command);

            command.CommandText = $"update{Environment.NewLine}{from}set {set}{where}";

            return command;
        }
        public override IDbCommand Delete(IQueryTable querytable, int maxdepth = 1)
        {
            var command = GetCommand();

            var querycolumns = GetQueryColumns(querytable, null, null, maxdepth, 0);
            var queryjoins = GetQueryJoins(querytable, querytable.Name, maxdepth, 0);

            var table = $"{SyntaxSign.AliasEnclosureTableOpen}{querytable.Name}{SyntaxSign.AliasEnclosureTableClose}{Environment.NewLine}";

            var from = GetQueryFrom(queryjoins, querytable.Name);

            var where = GetQueryWhere(querycolumns, command);

            command.CommandText = $"delete{Environment.NewLine}{table}from {from}{where}";

            return command;
        }

        public override IDbCommand Select(IEntityTable<T> entitytable)
        {
            var command = GetCommand();

            var columns = GetEntitySelectColumns(entitytable.Columns, command);

            var table = $"{entitytable.Name}{Environment.NewLine}";

            var where = GetEntityWhere(entitytable.Columns.Where(c => c.IsPrimaryKey).ToList(), command);

            command.CommandText = $"select{Environment.NewLine}{columns}from {table}{where}";

            return command;
        }

        public override IDbCommand Insert(IEntityTable<T> entitytable)
        {
            var command = GetCommand();

            var table = $"{entitytable.Name}{Environment.NewLine}";

            var insert = GetEntityInsertColumns(entitytable.Columns.Where(c => !c.IsIdentity).ToList(), command);

            var values = GetEntityInsertValues(entitytable.Columns.Where(c => !c.IsIdentity).ToList(), command);

            command.CommandText = $"insert{Environment.NewLine}into {table}{insert}values {values}";

            var output = string.Empty;

            foreach (var c in entitytable.Columns.Where(c => c.IsIdentity).ToList())
            {
                output += $"{(string.IsNullOrWhiteSpace(output) ? "SELECT " : ", ")}LAST_INSERT_ID()";
            }

            command.CommandText += $";{Environment.NewLine}{output}";

            return command;
        }
        public override IDbCommand Update(IEntityTable<T> entitytable)
        {
            var command = GetCommand();

            var table = $"{entitytable.Name}{Environment.NewLine}";

            var set = GetEntityUpdateSet(entitytable.Columns.Where(c => !c.IsIdentity && c.Value != c.DbValue).ToList(), command);

            var where = GetEntityWhere(entitytable.Columns.Where(c => c.IsPrimaryKey && c.DbValue != null).ToList(), command);

            command.CommandText = $"update{Environment.NewLine}{table}set {set}{where}";

            return command;
        }
        public override IDbCommand Delete(IEntityTable<T> entitytable)
        {
            var command = GetCommand();

            var table = $"{entitytable.Name}{Environment.NewLine}";

            var where = GetEntityWhere(entitytable.Columns.Where(c => c.IsPrimaryKey && c.DbValue != null).ToList(), command);

            command.CommandText = $"delete{Environment.NewLine}from {table}{where}";

            return command;
        }
    }
}
