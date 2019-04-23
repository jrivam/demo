using library.Impl.Persistence;
using Library.Impl.Persistence.Sql;
using Library.Impl.Persistence.Sql.Builder;
using Library.Impl.Persistence.Sql.Factory;
using Library.Impl.Persistence.Sql.Repository;
using Library.Interface.Entities;
using Library.Interface.Entities.Reader;
using Library.Interface.Persistence.Mapper;
using Library.Interface.Persistence.Sql.Builder;
using Library.Interface.Persistence.Sql.Database;
using Library.Interface.Persistence.Sql.Providers;
using Library.Interface.Persistence.Sql.Repository;
using Library.Interface.Persistence.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace Library.Impl.Persistence.Table
{
    public class RepositoryTable<T, U> : RepositoryMapper<T, U>, IRepositoryTable<T, U> 
        where T : IEntity
        where U : ITableData<T, U>
    {
        protected readonly ISqlCommandBuilder _sqlcommandbuilder;
        protected readonly ISqlBuilderTable _sqlbuilder;

        public RepositoryTable(ISqlRepository<T> sqlrepository, ISqlRepositoryBulk sqlrepositorybulk,
            IMapper<T, U> mapper,
            ISqlCommandBuilder sqlcommandbuilder,
            ISqlBuilderTable sqlbuilder)
            : base(sqlrepository, sqlrepositorybulk,
                  mapper)
        {
            _sqlcommandbuilder = sqlcommandbuilder;
            _sqlbuilder = sqlbuilder;
        }

        public RepositoryTable(ISqlSyntaxSign sqlsyntaxsign,
            IMapper<T, U> mapper, 
            ISqlCommandBuilder sqlcommandbuilder,
            ISqlRepository<T> sqlrepository, ISqlRepositoryBulk sqlrepositorybulk)
            : this(sqlrepository, sqlrepositorybulk,
                  mapper, 
                  sqlcommandbuilder,
                  new SqlBuilderTable(sqlsyntaxsign))
        {
        }
        public RepositoryTable(IReader<T> reader, IMapper<T, U> mapper,            
            ISqlSyntaxSign sqlsyntaxsign,             
            ISqlCommandBuilder sqlcommandbuilder,
            ISqlCreator sqlcreator)
            : this(sqlsyntaxsign, 
                  mapper, 
                  sqlcommandbuilder,
                  new SqlRepository<T>(sqlcreator, reader), new SqlRepositoryBulk(sqlcreator))
        {
        }
        public RepositoryTable(IReader<T> reader, IMapper<T, U> mapper, 
            ConnectionStringSettings connectionstringsettings)
            : this(reader, mapper,                  
                  SqlSyntaxSignFactory.Create(connectionstringsettings),
                  SqlCommandBuilderFactory.Create(connectionstringsettings),
                  new SqlCreator(connectionstringsettings))
        {
        }

        public RepositoryTable(IReader<T> reader, IMapper<T, U> mapper, 
            string appconnectionstringname)
            : this(reader, mapper,
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

                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "Select", "No SelectDbCommand defined.") } }, default(U));
            }

            var parameters = new List<SqlParameter>();

            var selectcommand = _sqlcommandbuilder.Select(_sqlbuilder.GetSelectColumns(table.Columns),
                $"{table.Description.Name}",
                _sqlbuilder.GetWhere(table.Columns.Where(c => c.IsPrimaryKey).ToList(), parameters), 1);

            return Select(table, selectcommand, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Select(U table, (string commandtext, CommandType commandtype, IList<SqlParameter> parameters) dbcommand)
        {
            foreach (var p in _sqlbuilder.GetParameters(table.Columns.Where(c => c.IsPrimaryKey).Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), dbcommand.parameters)) ;

            return Select(table, dbcommand.commandtext, dbcommand.commandtype, dbcommand.parameters);
        }
        public virtual (Result result, U data) Select(U table, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var executequery = Select(commandtext, commandtype, parameters, 1);

            if (executequery.result.Success && executequery.entities?.Count() > 0)
            {
                table.Entity = executequery.entities.FirstOrDefault();

                _mapper.Clear(table);
                Map(table, 1);

                return (executequery.result, table);
            }

            return (executequery.result, default(U));
        }

        public virtual (Result result, U data) Insert(U table, bool usedbcommand = false)
        {
            if (UseDbCommand(table?.UseDbCommand ?? false, table?.InsertDbCommand?.usedbcommand ?? false, usedbcommand))
            {
                if (table.InsertDbCommand != null)
                {
                    return Insert(table, table.InsertDbCommand.Value.dbcommand);
                }

                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "Insert", "No InsertDbCommand defined.") } }, default(U));
            }

            var parameters = new List<SqlParameter>();

            var output = string.Empty;

            foreach (var c in table.Columns.Where(c => c.IsIdentity).ToList())
            {
                output += $"{(string.IsNullOrWhiteSpace(output) ? " " : ", ")}";
            }

            var insertcommand = _sqlcommandbuilder.Insert($"{table.Description.Name}",
                _sqlbuilder.GetInsertColumns(table.Columns.Where(c => !c.IsIdentity).ToList()),
                _sqlbuilder.GetInsertValues(table.Columns.Where(c => !c.IsIdentity).ToList(), parameters),
                output);

            return Insert(table, insertcommand, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Insert(U table, (string commandtext, CommandType commandtype, IList<SqlParameter> parameters) dbcommand)
        {
            foreach (var p in _sqlbuilder.GetParameters(table.Columns.Where(c => !c.IsIdentity).Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), dbcommand.parameters)) ;

            return Insert(table, dbcommand.commandtext, dbcommand.commandtype, dbcommand.parameters);
        }
        public virtual (Result result, U data) Insert(U table, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var executescalar = Insert(commandtext, commandtype, parameters);

            if (executescalar.result.Success && executescalar.scalar != null)
            {
                table.Entity.Id = Convert.ToInt32(executescalar.scalar);

                Map(table, 1);

                return (executescalar.result, table);
            }

            return (executescalar.result, default(U));
        }

        public virtual (Result result, U data) Update(U table, bool usedbcommand = false)
        {
            if (UseDbCommand(table?.UseDbCommand ?? false, table?.UpdateDbCommand?.usedbcommand ?? false, usedbcommand))
            {
                if (table.UpdateDbCommand != null)
                {
                    return Update(table, table.UpdateDbCommand.Value.dbcommand);
                }

                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "Update", "No UpdateDbCommand defined.") } }, default(U));
            }

            var parameters = new List<SqlParameter>();

            var updatecommand = _sqlcommandbuilder.Update($"{table.Description.Name}",
                $"{table.Description.Name}",
                _sqlbuilder.GetUpdateSet(table.Columns.Where(c => !c.IsIdentity && c.Value != c.DbValue).Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), parameters),
                _sqlbuilder.GetWhere(table.Columns.Where(c => c.IsPrimaryKey && c.DbValue != null).ToList(), parameters));

            return Update(table, updatecommand, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Update(U table, (string commandtext, CommandType commandtype, IList<SqlParameter> parameters) dbcommand)
        {
            foreach (var p in _sqlbuilder.GetParameters(table.Columns.Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), dbcommand.parameters)) ;

            return Update(table, dbcommand.commandtext, dbcommand.commandtype, dbcommand.parameters);
        }
        public virtual (Result result, U data) Update(U table, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var executenonquery = Update(commandtext, commandtype, parameters);

            if (executenonquery.result.Success && executenonquery.rows > 0)
            {
                Map(table, 1);

                return (executenonquery.result, table);
            }

            return (executenonquery.result, default(U));
        }

        public virtual (Result result, U data) Delete(U table, bool usedbcommand = false)
        {
            if (UseDbCommand(table?.UseDbCommand ?? false, table?.DeleteDbCommand?.usedbcommand ?? false, usedbcommand))
            {
                if (table.DeleteDbCommand != null)
                {
                    return Delete(table, table.DeleteDbCommand.Value.dbcommand);
                }

                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "Delete", "No DeleteDbCommand defined.") } }, default(U));
            }

            var parameters = new List<SqlParameter>();

            var deletecommand = _sqlcommandbuilder.Delete($"{table.Description.Name}",
                $"{table.Description.Name}",
                _sqlbuilder.GetWhere(table.Columns.Where(c => c.IsPrimaryKey && c.DbValue != null).ToList(), parameters));

            return Delete(table, deletecommand, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Delete(U table, (string commandtext, CommandType commandtype, IList<SqlParameter> parameters) dbcommand)
        {
            foreach (var p in _sqlbuilder.GetParameters(table.Columns.Where(c => c.IsPrimaryKey).Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), dbcommand.parameters)) ;

            return Delete(table, dbcommand.commandtext, dbcommand.commandtype, dbcommand.parameters);
        }
        public virtual (Result result, U data) Delete(U table, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var executenonquery = Delete(commandtext, commandtype, parameters);

            if (executenonquery.result.Success && executenonquery.rows > 0)
            {
                return (executenonquery.result, table);
            }

            return (executenonquery.result, default(U));
        }
    }
}
