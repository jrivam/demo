﻿using library.Interface.Data;
using library.Interface.Data.Sql;
using library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace library.Impl.Data.Sql.Builder
{
    public class SqlServerBuilder<T> : AbstractSqlBuilder<T> where T : IEntity
    {
        protected override IDictionary<DbType, object> DbTypeToSqlType
        {
            get
            {
                return new Dictionary<DbType, object>()
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

        public SqlServerBuilder(ConnectionStringSettings connectionstringsettings, ISqlSyntaxSign syntaxsign)
            : base(connectionstringsettings, syntaxsign)
        {

        }

        public override IDbCommand Select(IQueryTable query, int maxdepth = 1, int top = 0)
        {
            var command = GetCommand();

            var querycolumns = GetQueryColumns(query, null, null, maxdepth, 0);
            var queryjoins = GetQueryJoins(query, query.Name, maxdepth, 0);

            var columns = GetQuerySelectColumns(querycolumns, command);

            var from = GetQueryFrom(queryjoins, query.Name);

            var where = GetQueryWhere(querycolumns, command);

            command.CommandText = $"select{Environment.NewLine}{columns}from {from}{where}";
            command.CommandText = command.CommandText.Replace("select", $"select{(top > 0 ? $" top {top.ToString()}" : "")}");

            return command;
        }

        public override IDbCommand Update(IEntityTable<T> entity, IQueryTable query, int maxdepth = 1)
        {
            var command = GetCommand();

            var querycolumns = GetQueryColumns(query, null, null, maxdepth, 0);
            var queryjoins = GetQueryJoins(query, query.Name, maxdepth, 0);

            var set = GetEntityUpdateSet(entity.Columns.Where(c => !c.IsIdentity && c.Value != c.DbValue).ToList(), command);

            var table = $"{SyntaxSign.AliasEnclosureTableOpen}{query.Name}{SyntaxSign.AliasEnclosureTableClose}";

            var from = GetQueryFrom(queryjoins, entity.Name);

            var where = GetQueryWhere(querycolumns, command);

            command.CommandText = $"update{Environment.NewLine}{table}set {set}from {from}{where}";

            return command;
        }
        public override IDbCommand Delete(IQueryTable query, int maxdepth = 1)
        {
            var command = GetCommand();

            var querycolumns = GetQueryColumns(query, null, null, maxdepth, 0);
            var queryjoins = GetQueryJoins(query, query.Name, maxdepth, 0);

            var table = $"{SyntaxSign.AliasEnclosureTableOpen}{query.Name}{SyntaxSign.AliasEnclosureTableClose}{Environment.NewLine}";

            var from = GetQueryFrom(queryjoins, query.Name);

            var where = GetQueryWhere(querycolumns, command);

            command.CommandText = $"delete{Environment.NewLine}{table}from {from}{where}";

            return command;
        }

        public override IDbCommand Select(IEntityTable<T> entity)
        {
            var command = GetCommand();

            var columns = GetEntitySelectColumns(entity.Columns, command);

            var table = $"{entity.Name}{Environment.NewLine}";

            var where = GetEntityWhere(entity.Columns.Where(c => c.IsPrimaryKey).ToList(), command);

            command.CommandText = $"select{Environment.NewLine}{columns}from {table}{where}";

            return command;
        }

        public override IDbCommand Insert(IEntityTable<T> entity)
        {
            var command = GetCommand();

            var table = $"{entity.Name}{Environment.NewLine}";

            var columns = GetEntityInsertColumns(entity.Columns.Where(c => !c.IsIdentity).ToList(), command);

            var values = GetEntityInsertValues(entity.Columns.Where(c => !c.IsIdentity).ToList(), command);

            command.CommandText = $"insert{Environment.NewLine}into {table}{columns}values {values}";

            var output = string.Empty;

            foreach (var c in entity.Columns.Where(c => c.IsIdentity).ToList())
            {
                output += $"{(string.IsNullOrWhiteSpace(output) ? "SELECT " : ", ")}SCOPE_IDENTITY()";
            }

            command.CommandText += $";{Environment.NewLine}{output}";

            return command;
        }
        public override IDbCommand Update(IEntityTable<T> entity)
        {
            var command = GetCommand();

            var table = $"{entity.Name}{Environment.NewLine}";

            var set = GetEntityUpdateSet(entity.Columns.Where(c => !c.IsIdentity && c.Value != c.DbValue).ToList(), command);

            var where = GetEntityWhere(entity.Columns.Where(c => c.IsPrimaryKey && c.DbValue != null).ToList(), command);

            command.CommandText = $"update{Environment.NewLine}{table}set {set}{where}";

            return command;
        }
        public override IDbCommand Delete(IEntityTable<T> entity)
        {
            var command = GetCommand();

            var table = $"{entity.Name}{Environment.NewLine}";

            var where = GetEntityWhere(entity.Columns.Where(c => c.IsPrimaryKey && c.DbValue != null).ToList(), command);

            command.CommandText = $"delete{Environment.NewLine}from {table}{where}";

            return command;
        }
    }
}
