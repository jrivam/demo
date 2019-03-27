﻿using library.Impl.Data;
using Library.Impl.Data.Sql;
using Library.Impl.Data.Sql.Builder;
using Library.Impl.Data.Sql.Factory;
using Library.Impl.Data.Sql.Repository;
using Library.Interface.Data.Mapper;
using Library.Interface.Data.Sql.Builder;
using Library.Interface.Data.Sql.Database;
using Library.Interface.Data.Sql.Providers;
using Library.Interface.Data.Sql.Repository;
using Library.Interface.Data.Table;
using Library.Interface.Entities;
using Library.Interface.Entities.Reader;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace Library.Impl.Data.Table
{
    public class RepositoryTable<T, U> : Repository<T, U>, IRepositoryTable<T, U> 
        where T : IEntity
        where U : ITableData<T, U>
    {
        protected readonly ISqlBuilderTable _builder;

        public RepositoryTable(ISqlBuilderTable builder,
            ISqlRepository<T> repository, ISqlRepositoryBulk repositorybulk,
            IMapperRepository<T, U> mapper, ISqlSyntaxSign syntaxsign, ISqlCommandBuilder commandbuilder)
            : base(repository, repositorybulk,
                  mapper, syntaxsign, commandbuilder)
        {
            _builder = builder;
        }

        public RepositoryTable(ISqlRepository<T> repository, ISqlRepositoryBulk repositorybulk,
            IMapperRepository<T, U> mapper, ISqlSyntaxSign syntaxsign, ISqlCommandBuilder commandbuilder)
            : this(new SqlBuilderTable(syntaxsign),
                  repository, repositorybulk,
                  mapper, syntaxsign, commandbuilder)
        {
        }
        public RepositoryTable(IReaderEntity<T> reader, IMapperRepository<T, U> mapper, 
            ISqlCreator creator, ISqlSyntaxSign syntaxsign, ISqlCommandBuilder commandbuilder)
            : this(new SqlRepository<T>(creator, reader), new SqlRepositoryBulk(creator),
                  mapper, syntaxsign, commandbuilder)
        {
        }
        public RepositoryTable(IReaderEntity<T> reader, IMapperRepository<T, U> mapper, 
            ConnectionStringSettings connectionstringsettings)
            : this(reader, mapper,
                  new SqlCreator(connectionstringsettings),
                  SqlSyntaxSignFactory.Create(connectionstringsettings),
                  SqlCommandBuilderFactory.Create(connectionstringsettings))
        {
        }

        public RepositoryTable(IReaderEntity<T> reader, IMapperRepository<T, U> mapper, 
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

            var select = _commandbuilder.Select(_builder.GetSelectColumns(table.Columns),
                $"{table.Description.Name}",
                _builder.GetWhere(table.Columns.Where(c => c.IsPrimaryKey).ToList(), parameters), 1);

            return Select(table, select, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Select(U table, (string commandtext, CommandType commandtype, IList<SqlParameter> parameters) dbcommand)
        {
            foreach (var p in _builder.GetParameters(table.Columns.Where(c => c.IsPrimaryKey).Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), dbcommand.parameters)) ;

            return Select(table, dbcommand.commandtext, dbcommand.commandtype, dbcommand.parameters);
        }
        public virtual (Result result, U data) Select(U table, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var executequery = _repository.ExecuteQuery(_syntaxsign.AliasSeparatorColumn, commandtext, commandtype, parameters, 1);

            if (executequery.result.Success && executequery.entities?.Count() > 0)
            {
                table.Entity = executequery.entities.FirstOrDefault();

                _mapper.Clear(table, 1, 0);
                _mapper.Map(table, 1, 0);

                _mapper.Extra(table, 1, 0);

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

            var insert = _commandbuilder.Insert($"{table.Description.Name}",
                _builder.GetInsertColumns(table.Columns.Where(c => !c.IsIdentity).ToList()),
                _builder.GetInsertValues(table.Columns.Where(c => !c.IsIdentity).ToList(), parameters),
                output);

            return Insert(table, insert, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Insert(U table, (string commandtext, CommandType commandtype, IList<SqlParameter> parameters) dbcommand)
        {
            foreach (var p in _builder.GetParameters(table.Columns.Where(c => !c.IsIdentity).Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), dbcommand.parameters)) ;

            return Insert(table, dbcommand.commandtext, dbcommand.commandtype, dbcommand.parameters);
        }
        public virtual (Result result, U data) Insert(U table, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var executescalar = _repositorybulk.ExecuteScalar(commandtext, commandtype, parameters);

            if (executescalar.result.Success)
            {
                if (executescalar.scalar != null)
                {
                    table.Entity.Id = Convert.ToInt32(executescalar.scalar);

                    _mapper.Map(table, 1, 0);

                    return (executescalar.result, table);
                }
                else
                {
                    executescalar.result.Messages.Add((ResultCategory.Information, "Insert", "No rows affected."));
                }
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

            var update = _commandbuilder.Update($"{table.Description.Name}",
                $"{table.Description.Name}",
                _builder.GetUpdateSet(table.Columns.Where(c => !c.IsIdentity && c.Value != c.DbValue).Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), parameters, _syntaxsign.UpdateSetUseAlias),
                _builder.GetWhere(table.Columns.Where(c => c.IsPrimaryKey && c.DbValue != null).ToList(), parameters, _syntaxsign.UpdateWhereUseAlias));

            return Update(table, update, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Update(U table, (string commandtext, CommandType commandtype, IList<SqlParameter> parameters) dbcommand)
        {
            foreach (var p in _builder.GetParameters(table.Columns.Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), dbcommand.parameters)) ;

            return Update(table, dbcommand.commandtext, dbcommand.commandtype, dbcommand.parameters);
        }
        public virtual (Result result, U data) Update(U table, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var executenonquery = _repositorybulk.ExecuteNonQuery(commandtext, commandtype, parameters);

            if (executenonquery.result.Success)
            {
                if (executenonquery.rows > 0)
                {
                    _mapper.Map(table, 1, 0);

                    return (executenonquery.result, table);
                }
                else
                {
                    executenonquery.result.Messages.Add((ResultCategory.Information, "Update", "No rows affected"));
                }
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

            var delete = _commandbuilder.Delete($"{table.Description.Name}",
                $"{table.Description.Name}",
                _builder.GetWhere(table.Columns.Where(c => c.IsPrimaryKey && c.DbValue != null).ToList(), parameters));

            return Delete(table, delete, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Delete(U table, (string commandtext, CommandType commandtype, IList<SqlParameter> parameters) dbcommand)
        {
            foreach (var p in _builder.GetParameters(table.Columns.Where(c => c.IsPrimaryKey).Select(x => (x.Table.Description, x.Description, x.Type, x.Value)).ToList(), dbcommand.parameters)) ;

            return Delete(table, dbcommand.commandtext, dbcommand.commandtype, dbcommand.parameters);
        }
        public virtual (Result result, U data) Delete(U table, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var executenonquery = _repositorybulk.ExecuteNonQuery(commandtext, commandtype, parameters);

            if (executenonquery.result.Success)
            {
                if (executenonquery.rows > 0)
                {
                    return (executenonquery.result, table);
                }
                else
                {
                    executenonquery.result.Messages.Add((ResultCategory.Information, "Delete", "No rows affected"));
                }
            }

            return (executenonquery.result, default(U));
        }
    }
}