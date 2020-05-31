using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Impl.Persistence.Sql.Builder;
using jrivam.Library.Impl.Persistence.Sql.Factory;
using jrivam.Library.Impl.Persistence.Sql.Repository;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Entities.Reader;
using jrivam.Library.Interface.Persistence.Mapper;
using jrivam.Library.Interface.Persistence.Sql;
using jrivam.Library.Interface.Persistence.Sql.Builder;
using jrivam.Library.Interface.Persistence.Sql.Database;
using jrivam.Library.Interface.Persistence.Sql.Providers;
using jrivam.Library.Interface.Persistence.Sql.Repository;
using jrivam.Library.Interface.Persistence.Table;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;

namespace jrivam.Library.Impl.Persistence.Table
{
    public class RepositoryTable<T, U> : Repository<T>, IRepositoryTable<T, U> 
        where T : IEntity
        where U : ITableData<T, U>
    {
        protected readonly ISqlCommandBuilder _sqlcommandbuilder;
        protected readonly ISqlBuilderTable _sqlbuilder;

        public RepositoryTable(ISqlCommandExecutor<T> sqlcommandexecutor, ISqlCommandExecutorBulk sqlcommandexecutorbulk,
            ISqlCommandBuilder sqlcommandbuilder,
            ISqlBuilderTable sqlbuilder)
            : base(sqlcommandexecutor, sqlcommandexecutorbulk)
        {
            _sqlcommandbuilder = sqlcommandbuilder;
            _sqlbuilder = sqlbuilder;
        }

        public RepositoryTable(ISqlSyntaxSign sqlsyntaxsign,
            ISqlCommandBuilder sqlcommandbuilder,
            ISqlCommandExecutor<T> sqlcommandexecutor, ISqlCommandExecutorBulk sqlcommandexecutorbulk)
            : this(sqlcommandexecutor, sqlcommandexecutorbulk,
                  sqlcommandbuilder,
                  new SqlBuilderTable(sqlsyntaxsign))
        {
        }
        public RepositoryTable(IReader<T> reader,       
            ISqlSyntaxSign sqlsyntaxsign,             
            ISqlCommandBuilder sqlcommandbuilder,
            ISqlCreator sqlcreator)
            : this(sqlsyntaxsign, 
                  sqlcommandbuilder,
                  new SqlCommandExecutor<T>(sqlcreator, reader), new SqlCommandExecutorBulk(sqlcreator))
        {
        }
        public RepositoryTable(IReader<T> reader,
            ConnectionStringSettings connectionstringsettings)
            : this(reader,                
                  SqlSyntaxSignFactory.Create(connectionstringsettings),
                  SqlCommandBuilderFactory.Create(connectionstringsettings),
                  new SqlCreator(connectionstringsettings))
        {
        }

        public RepositoryTable(IReader<T> reader,
            string appconnectionstringname)
            : this(reader,
                  ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appconnectionstringname]])
        {
        }

        protected virtual bool UseDbCommand(bool classusedbcommand, bool propertyusedbcommand, bool methodusedbcommand)
        {
            bool configusedbcommand = Convert.ToBoolean(ConfigurationManager.AppSettings["database.forceusedbcommand"]);

            return (methodusedbcommand || propertyusedbcommand || classusedbcommand || configusedbcommand);
        } 

        public virtual (Result result, U data) Select(U table, bool usedbcommand = false)
        {
            if (UseDbCommand(table?.UseDbCommand ?? false, table?.SelectDbCommand?.usedbcommand ?? false, usedbcommand))
            {
                if (table.SelectDbCommand != null)
                {
                    return Select(table, table.SelectDbCommand.Value.dbcommand);
                }

                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, nameof(Select), "No SelectDbCommand defined.") } }, default(U));
            }

            var parameters = new List<SqlParameter>();

            var selectcommandtext = _sqlcommandbuilder.Select(_sqlbuilder.GetSelectColumns(table.Columns),
                $"{table.Description.DbName}",
                _sqlbuilder.GetWhere(table.Columns.Where(c => c.IsPrimaryKey).ToList(), parameters), 1);

            return Select(table, selectcommandtext, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Select(U table, ISqlCommand dbcommand)
        {
            foreach (var p in _sqlbuilder.GetParameters(table.Columns.Where(c => c.IsPrimaryKey).Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), dbcommand.Parameters)) ;

            return Select(table, dbcommand.Text, dbcommand.Type, dbcommand.Parameters);
        }
        public virtual (Result result, U data) Select(U table, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var select = Select(commandtext, commandtype, parameters, 1, new Collection<T> { table.Entity });
            if (select.result.Success && select.entities?.Count() > 0)
            {
                table.Entity = select.entities.FirstOrDefault();

                table.Clear(table);
                table.Map(table, 1);

                return (select.result, table);
            }

            return (select.result, default(U));
        }

        public virtual (Result result, U data) Insert(U table, bool usedbcommand = false)
        {
            if (UseDbCommand(table?.UseDbCommand ?? false, table?.InsertDbCommand?.usedbcommand ?? false, usedbcommand))
            {
                if (table.InsertDbCommand != null)
                {
                    return Insert(table, table.InsertDbCommand.Value.dbcommand);
                }

                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, nameof(Insert), "No InsertDbCommand defined.") } }, default(U));
            }

            var parameters = new List<SqlParameter>();

            var output = string.Empty;

            foreach (var c in table.Columns.Where(c => c.IsIdentity).ToList())
            {
                output += $"{(string.IsNullOrWhiteSpace(output) ? " " : ", ")}";
            }

            var insertcommandtext = _sqlcommandbuilder.Insert($"{table.Description.DbName}",
                _sqlbuilder.GetInsertColumns(table.Columns.Where(c => !c.IsIdentity).ToList()),
                _sqlbuilder.GetInsertValues(table.Columns.Where(c => !c.IsIdentity).ToList(), parameters),
                output);

            return Insert(table, insertcommandtext, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Insert(U table, ISqlCommand dbcommand)
        {
            foreach (var p in _sqlbuilder.GetParameters(table.Columns.Where(c => !c.IsIdentity).Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), dbcommand.Parameters)) ;

            return Insert(table, dbcommand.Text, dbcommand.Type, dbcommand.Parameters);
        }
        public virtual (Result result, U data) Insert(U table, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var insert = Insert(commandtext, commandtype, parameters);
            if (insert.result.Success && insert.scalar != null)
            {
                table.Entity.Id = Convert.ToInt32(insert.scalar);

                table.Map(table, 1);

                return (insert.result, table);
            }

            return (insert.result, default(U));
        }

        public virtual (Result result, U data) Update(U table, bool usedbcommand = false)
        {
            if (UseDbCommand(table?.UseDbCommand ?? false, table?.UpdateDbCommand?.usedbcommand ?? false, usedbcommand))
            {
                if (table.UpdateDbCommand != null)
                {
                    return Update(table, table.UpdateDbCommand.Value.dbcommand);
                }

                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, nameof(Update), "No UpdateDbCommand defined.") } }, default(U));
            }

            var parameters = new List<SqlParameter>();

            var updatecommandtext = _sqlcommandbuilder.Update($"{table.Description.DbName}",
                $"{table.Description.DbName}",
                _sqlbuilder.GetUpdateSet(table.Columns.Where(c => !c.IsIdentity && c.Value != c.DbValue).Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), parameters),
                _sqlbuilder.GetWhere(table.Columns.Where(c => c.IsPrimaryKey && c.DbValue != null).ToList(), parameters));

            return Update(table, updatecommandtext, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Update(U table, ISqlCommand dbcommand)
        {
            foreach (var p in _sqlbuilder.GetParameters(table.Columns.Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), dbcommand.Parameters)) ;

            return Update(table, dbcommand.Text, dbcommand.Type, dbcommand.Parameters);
        }
        public virtual (Result result, U data) Update(U table, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var update = Update(commandtext, commandtype, parameters);
            if (update.result.Success && update.rows > 0)
            {
                table.Map(table, 1);

                return (update.result, table);
            }

            return (update.result, default(U));
        }

        public virtual (Result result, U data) Delete(U table, bool usedbcommand = false)
        {
            if (UseDbCommand(table?.UseDbCommand ?? false, table?.DeleteDbCommand?.usedbcommand ?? false, usedbcommand))
            {
                if (table.DeleteDbCommand != null)
                {
                    return Delete(table, table.DeleteDbCommand.Value.dbcommand);
                }

                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, nameof(Delete), "No DeleteDbCommand defined.") } }, default(U));
            }

            var parameters = new List<SqlParameter>();

            var deletecommandtext = _sqlcommandbuilder.Delete($"{table.Description.DbName}",
                $"{table.Description.DbName}",
                _sqlbuilder.GetWhere(table.Columns.Where(c => c.IsPrimaryKey && c.DbValue != null).ToList(), parameters));

            return Delete(table, deletecommandtext, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Delete(U table, ISqlCommand dbcommand)
        {
            foreach (var p in _sqlbuilder.GetParameters(table.Columns.Where(c => c.IsPrimaryKey).Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), dbcommand.Parameters)) ;

            return Delete(table, dbcommand.Text, dbcommand.Type, dbcommand.Parameters);
        }
        public virtual (Result result, U data) Delete(U table, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var delete = Delete(commandtext, commandtype, parameters);
            if (delete.result.Success && delete.rows > 0)
            {
                return (delete.result, table);
            }

            return (delete.result, default(U));
        }
    }
}
