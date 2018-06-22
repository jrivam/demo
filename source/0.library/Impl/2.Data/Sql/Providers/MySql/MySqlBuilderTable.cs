using library.Impl.Data.Sql.Builder;
using library.Interface.Data.Sql;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace library.Impl.Data.Sql.Providers.MySql
{
    public class MySqlBuilderTable<T> : AbstractSqlBuilderTable<T> 
        where T : IEntity
    {
        public MySqlBuilderTable(ISqlSyntaxSign syntaxsign)
            : base(syntaxsign)
        {

        }

        public override (string commandtext, IList<SqlParameter> parameters) Select(ITableRepositoryProperties<T> entitytable)
        {
            string commandtext = string.Empty;
            IList<SqlParameter> parameters = new List<SqlParameter>();

            var columns = GetEntitySelectColumns(entitytable.Columns);

            var table = $"{entitytable.Description.Name}{Environment.NewLine}";

            var where = GetEntityWhere(entitytable.Columns.Where(c => c.IsPrimaryKey).ToList(), parameters);

            commandtext = $"select{Environment.NewLine}{columns}from {table}{where}";

            return (commandtext, parameters);
        }

        public override (string commandtext, IList<SqlParameter> parameters) Insert(ITableRepositoryProperties<T> entitytable)
        {
            string commandtext = string.Empty;
            IList<SqlParameter> parameters = new List<SqlParameter>();

            var table = $"{entitytable.Description.Name}{Environment.NewLine}";

            var insert = GetEntityInsertColumns(entitytable.Columns.Where(c => !c.IsIdentity).ToList());

            var values = GetEntityInsertValues(entitytable.Columns.Where(c => !c.IsIdentity).ToList(), parameters);

            commandtext = $"insert{Environment.NewLine}into {table}{insert}values {values}";

            var output = string.Empty;

            foreach (var c in entitytable.Columns.Where(c => c.IsIdentity).ToList())
            {
                output += $"{(string.IsNullOrWhiteSpace(output) ? "SELECT " : ", ")}LAST_INSERT_ID()";
            }

            commandtext += $";{Environment.NewLine}{output}";

            return (commandtext, parameters);
        }
        public override (string commandtext, IList<SqlParameter> parameters) Update(ITableRepositoryProperties<T> entitytable)
        {
            string commandtext = string.Empty;
            IList<SqlParameter> parameters = new List<SqlParameter>();

            var table = $"{entitytable.Description.Name}{Environment.NewLine}";

            var set = GetUpdateSet(entitytable.Columns.Where(c => !c.IsIdentity && c.Value != c.DbValue).ToList(), parameters);

            var where = GetEntityWhere(entitytable.Columns.Where(c => c.IsPrimaryKey && c.DbValue != null).ToList(), parameters);

            commandtext = $"update{Environment.NewLine}{table}set {set}{where}";

            return (commandtext, parameters);
        }
        public override (string commandtext, IList<SqlParameter> parameters) Delete(ITableRepositoryProperties<T> entitytable)
        {
            string commandtext = string.Empty;
            IList<SqlParameter> parameters = new List<SqlParameter>();

            var table = $"{entitytable.Description.Name}{Environment.NewLine}";

            var where = GetEntityWhere(entitytable.Columns.Where(c => c.IsPrimaryKey && c.DbValue != null).ToList(), parameters);

            commandtext = $"delete{Environment.NewLine}from {table}{where}";

            return (commandtext, parameters);
        }
    }
}
