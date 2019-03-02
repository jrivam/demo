﻿using library.Impl.Data.Sql;
using library.Impl.Data.Sql.Builder;
using library.Impl.Data.Sql.Factory;
using library.Impl.Entities.Repository;
using library.Interface.Data.Mapper;
using library.Interface.Data.Sql;
using library.Interface.Data.Sql.Builder;
using library.Interface.Data.Table;
using library.Interface.Entities;
using library.Interface.Entities.Reader;
using library.Interface.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace library.Impl.Data.Table
{
    public class RepositoryTable<T, U> : IRepositoryTable<T, U> 
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
    {
        protected readonly ISqlBuilderTable _builder;

        protected readonly IRepository<T> _repository;
        protected readonly IRepositoryBulk _repositorybulk;

        protected readonly IMapperRepository<T, U> _mapper;
        protected readonly ISqlSyntaxSign _syntaxsign;
        protected readonly ISqlCommandBuilder _commandbuilder;

        public RepositoryTable(ISqlBuilderTable builder,
            IRepository<T> repository, IRepositoryBulk repositorybulk,
            IMapperRepository<T, U> mapper, ISqlSyntaxSign syntaxsign, ISqlCommandBuilder commandbuilder)
        {
            _builder = builder;

            _repository = repository;
            _repositorybulk = repositorybulk;

            _mapper = mapper;
            _syntaxsign = syntaxsign;
            _commandbuilder = commandbuilder;
        }

        public RepositoryTable(IRepository<T> repository, IRepositoryBulk repositorybulk,
            IMapperRepository<T, U> mapper, ISqlSyntaxSign syntaxsign, ISqlCommandBuilder commandbuilder)
            : this(new SqlBuilderTable(syntaxsign),
                  repository, repositorybulk,
                  mapper, syntaxsign, commandbuilder)
        {
        }
        public RepositoryTable(IReaderEntity<T> reader, IMapperRepository<T, U> mapper, 
            ISqlCreator creator, ISqlSyntaxSign syntaxsign, ISqlCommandBuilder commandbuilder)
            : this(new Repository<T>(creator, reader), new RepositoryBulk(creator),
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

        public virtual (Result result, U data) Select(U data, bool usedbcommand = false)
        {
            if (UseDbCommand(data?.UseDbCommand ?? false, data?.SelectDbCommand?.usedbcommand ?? false, usedbcommand))
            {
                if (data.SelectDbCommand != null)
                {
                    return Select(data, data.SelectDbCommand.Value.dbcommand);
                }

                return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Error, "No SelectDbCommand defined.") } }, data);
            }

            var parameters = new List<SqlParameter>();

            IList<(ITableRepository table, ITableColumn column)> tablecolumns = data.Columns.Select(x => (data as ITableRepository, x)).ToList();

            var select = _commandbuilder.Select(_builder.GetSelectColumns(tablecolumns),
                $"{data.Description.Name}",
                _builder.GetWhere(tablecolumns.Where(c => c.column.IsPrimaryKey).ToList(), parameters), 1);

            return Select(data, select, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Select(U data, (string commandtext, CommandType commandtype, IList<SqlParameter> parameters) dbcommand)
        {
            IList<(ITableRepository table, ITableColumn column)> tablecolumns = data.Columns.Select(x => (data as ITableRepository, x)).ToList();

            foreach (var p in _builder.GetParameters(tablecolumns.Where(c => c.column.IsPrimaryKey).ToList(), dbcommand.parameters)) ;

            return Select(data, dbcommand.commandtext, dbcommand.commandtype, dbcommand.parameters);
        }
        public virtual (Result result, U data) Select(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var executequery = _repository.ExecuteQuery(_syntaxsign.AliasSeparatorColumn, commandtext, commandtype, parameters, 1, data.Entity != null ? new List<T> { data.Entity } : default(List<T>));

            if (executequery.result.Success && executequery.entities?.Count() > 0)
            {
                _mapper.Clear(data, 1, 0);
                _mapper.Map(data, 1, 0);

                return (executequery.result, data);
            }

            return (executequery.result, default(U));

        }

        public virtual (Result result, U data) Insert(U data, bool usedbcommand = false)
        {
            if (UseDbCommand(data?.UseDbCommand ?? false, data?.InsertDbCommand?.usedbcommand ?? false, usedbcommand))
            {
                if (data.InsertDbCommand != null)
                {
                    return Insert(data, data.InsertDbCommand.Value.dbcommand);
                }

                return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Error, "No InsertDbCommand defined.") } }, data);
            }

            var parameters = new List<SqlParameter>();

            var output = string.Empty;

            foreach (var c in data.Columns.Where(c => c.IsIdentity).ToList())
            {
                output += $"{(string.IsNullOrWhiteSpace(output) ? " " : ", ")}";
            }

            IList<(ITableRepository table, ITableColumn column)> tablecolumns = data.Columns.Select(x => (data as ITableRepository, x)).ToList();

            var insert = _commandbuilder.Insert($"{data.Description.Name}",
                _builder.GetInsertColumns(tablecolumns.Where(c => !c.column.IsIdentity).ToList()),
                _builder.GetInsertValues(tablecolumns.Where(c => !c.column.IsIdentity).ToList(), parameters),
                output);

            return Insert(data, insert, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Insert(U data, (string commandtext, CommandType commandtype, IList<SqlParameter> parameters) dbcommand)
        {
            IList<(ITableRepository table, ITableColumn column)> tablecolumns = data.Columns.Select(x => (data as ITableRepository, x)).ToList();

            foreach (var p in _builder.GetParameters(tablecolumns.Where(c => !c.column.IsIdentity).ToList(), dbcommand.parameters)) ;

            return Insert(data, dbcommand.commandtext, dbcommand.commandtype, dbcommand.parameters);
        }
        public virtual (Result result, U data) Insert(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var executescalar = _repositorybulk.ExecuteScalar(commandtext, commandtype, parameters);

            if (executescalar.result.Success)
            {
                if (executescalar.scalar != null)
                {
                    data.Entity.Id = Convert.ToInt32(executescalar.scalar);
                }
                else
                {
                    executescalar.result.Messages.Add((ResultCategory.Information, "Insert: no rows affected"));
                }
            }

            return (executescalar.result, data);
        }

        public virtual (Result result, U data) Update(U data, bool usedbcommand = false)
        {
            if (UseDbCommand(data?.UseDbCommand ?? false, data?.UpdateDbCommand?.usedbcommand ?? false, usedbcommand))
            {
                if (data.UpdateDbCommand != null)
                {
                    return Update(data, data.UpdateDbCommand.Value.dbcommand);
                }

                return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Error, "No UpdateDbCommand defined.") } }, data);
            }

            var parameters = new List<SqlParameter>();

            IList<(ITableRepository table, ITableColumn column)> tablecolumns = data.Columns.Select(x => (data as ITableRepository, x)).ToList();

            var update = _commandbuilder.Update($"{data.Description.Name}",
                $"{data.Description.Name}",
                _builder.GetUpdateSet(tablecolumns.Where(c => !c.column.IsIdentity && c.column.Value != c.column.DbValue).ToList(), parameters, _syntaxsign.UpdateSetUseAlias),
                _builder.GetWhere(tablecolumns.Where(c => c.column.IsPrimaryKey && c.column.DbValue != null).ToList(), parameters, _syntaxsign.UpdateWhereUseAlias));

            return Update(data, update, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Update(U data, (string commandtext, CommandType commandtype, IList<SqlParameter> parameters) dbcommand)
        {
            IList<(ITableRepository table, ITableColumn column)> tablecolumns = data.Columns.Select(x => (data as ITableRepository, x)).ToList();

            foreach (var p in _builder.GetParameters(tablecolumns.ToList(), dbcommand.parameters)) ;

            return Update(data, dbcommand.commandtext, dbcommand.commandtype, dbcommand.parameters);
        }
        public virtual (Result result, U data) Update(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var executenonquery = _repositorybulk.ExecuteNonQuery(commandtext, commandtype, parameters);

            if (executenonquery.result.Success)
            {
                if (executenonquery.rows <= 0)
                {
                    executenonquery.result.Messages.Add((ResultCategory.Information, "Update: no rows affected"));
                }
            }

            return (executenonquery.result, data);
        }

        public virtual (Result result, U data) Delete(U data, bool usedbcommand = false)
        {
            if (UseDbCommand(data?.UseDbCommand ?? false, data?.DeleteDbCommand?.usedbcommand ?? false, usedbcommand))
            {
                if (data.DeleteDbCommand != null)
                {
                    return Delete(data, data.DeleteDbCommand.Value.dbcommand);
                }

                return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Error, "No DeleteDbCommand defined.") } }, data);
            }

            var parameters = new List<SqlParameter>();

            IList<(ITableRepository table, ITableColumn column)> tablecolumns = data.Columns.Select(x => (data as ITableRepository, x)).ToList();

            var delete = _commandbuilder.Delete($"{data.Description.Name}",
                $"{data.Description.Name}",
                _builder.GetWhere(tablecolumns.Where(c => c.column.IsPrimaryKey && c.column.DbValue != null).ToList(), parameters));

            return Delete(data, delete, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Delete(U data, (string commandtext, CommandType commandtype, IList<SqlParameter> parameters) dbcommand)
        {
            IList<(ITableRepository table, ITableColumn column)> tablecolumns = data.Columns.Select(x => (data as ITableRepository, x)).ToList();

            foreach (var p in _builder.GetParameters(tablecolumns.Where(c => c.column.IsPrimaryKey).ToList(), dbcommand.parameters)) ;

            return Delete(data, dbcommand.commandtext, dbcommand.commandtype, dbcommand.parameters);
        }
        public virtual (Result result, U data) Delete(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            var executenonquery = _repositorybulk.ExecuteNonQuery(commandtext, commandtype, parameters);

            if (executenonquery.result.Success)
            {
                if (executenonquery.rows <= 0)
                {
                    executenonquery.result.Messages.Add((ResultCategory.Information, "Delete: no rows affected"));
                }
            }

            return (executenonquery.result, data);
        }
    }
}
