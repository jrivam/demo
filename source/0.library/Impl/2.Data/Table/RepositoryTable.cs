using library.Impl.Data.Sql;
using library.Impl.Data.Sql.Builder;
using library.Impl.Data.Sql.Factory;
using library.Interface.Data.Mapper;
using library.Interface.Data.Sql;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace library.Impl.Data.Table
{
    public class RepositoryTable<T, U> : Repository<T, U>, IRepositoryTable<T, U> 
        where T : IEntity
        where U : ITableRepositoryProperties<T>
    {
        protected readonly ISqlBuilderTable _builder;
        protected readonly ISqlCommandBuilder _commandbuilder;

        public RepositoryTable(ISqlCreator creator, IMapperRepository<T, U> mapper, 
            ISqlBuilderTable builder, ISqlCommandBuilder commandbuilder)
            : base(creator, mapper)
        {
            _builder = builder;
            _commandbuilder = commandbuilder;
        }
        public RepositoryTable(IMapperRepository<T, U> mapper, ConnectionStringSettings connectionstringsettings)
            : this(new SqlCreator(connectionstringsettings), mapper,
                  new SqlBuilderTable(SqlSyntaxSignFactory.Create(connectionstringsettings)),
                  SqlCommandBuilderFactory.Create(connectionstringsettings))
        {
        }
        public RepositoryTable(IMapperRepository<T, U> mapper, string appconnectionstringname)
            : this(mapper, ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appconnectionstringname]])
        {
        }

        protected virtual bool UseDbCommand(bool classusedbcommand, bool propertyusedbcommand, bool methodusedbcommand)
        {
            bool configusedbcommand = Convert.ToBoolean(ConfigurationManager.AppSettings["usedbcommand"]);

            if (!methodusedbcommand)
                if (!propertyusedbcommand)
                    if (!classusedbcommand)
                        if (!configusedbcommand)
                            return false;

            return true;
        }

        public virtual U Clear(U data, int maxdepth = 1)
        {
            return _mapper.Clear(data, maxdepth);
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

            var select = _commandbuilder.Select(_builder.GetSelectColumns(data.Columns),
                $"{data.Description.Name}",
                _builder.GetWhere(data.Columns.Where(c => c.IsPrimaryKey).ToList(), parameters), 1);

            return Select(data, select, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Select(U data, (string commandtext, CommandType commandtype, IList<SqlParameter> parameters) dbcommand)
        {
            foreach (var p in _builder.GetParameters(data.Columns.Where(c => c.IsPrimaryKey).ToList(), dbcommand.parameters)) ;

            var command = _creator.GetCommand(dbcommand.commandtext, dbcommand.commandtype, dbcommand.parameters);

            return Select(data, command);
        }
        public virtual (Result result, U data) Select(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            return Select(data, _creator.GetCommand(commandtext, commandtype, parameters));
        }
        public virtual (Result result, U data) Select(U data, IDbCommand command)
        {
            var executequery = ExecuteQuery(command, 1, data != null ? new List<U> { data } : default(List<U>));

            return (executequery.result, executequery.datas.FirstOrDefault());
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

            var insert = _commandbuilder.Insert($"{data.Description.Name}",
                _builder.GetInsertColumns(data.Columns.Where(c => !c.IsIdentity).ToList()),
                _builder.GetInsertValues(data.Columns.Where(c => !c.IsIdentity).ToList(), parameters),
                output);

            return Insert(data, insert, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Insert(U data, (string commandtext, CommandType commandtype, IList<SqlParameter> parameters) dbcommand)
        {
            foreach (var p in _builder.GetParameters(data.Columns.Where(c => !c.IsIdentity).ToList(), dbcommand.parameters)) ;

            var command = _creator.GetCommand(dbcommand.commandtext, dbcommand.commandtype, dbcommand.parameters);

            return Insert(data, command);
        }
        public virtual (Result result, U data) Insert(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            return Insert(data, _creator.GetCommand(commandtext, commandtype, parameters));
        }
        public virtual (Result result, U data) Insert(U data, IDbCommand command)
        {
            var executescalar = ExecuteScalar(command);

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

            var update = _commandbuilder.Update($"{data.Description.Name}",
                $"{data.Description.Name}",
                _builder.GetUpdateSet(data.Columns.Where(c => !c.IsIdentity && c.Value != c.DbValue).ToList(), parameters),
                _builder.GetWhere(data.Columns.Where(c => c.IsPrimaryKey && c.DbValue != null).ToList(), parameters));

            return Update(data, update, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Update(U data, (string commandtext, CommandType commandtype, IList<SqlParameter> parameters) dbcommand)
        {
            foreach (var p in _builder.GetParameters(data.Columns.ToList(), dbcommand.parameters)) ;

            var command = _creator.GetCommand(dbcommand.commandtext, dbcommand.commandtype, dbcommand.parameters);

            return Update(data, command);
        }
        public virtual (Result result, U data) Update(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            return Update(data, _creator.GetCommand(commandtext, commandtype, parameters));
        }
        public virtual (Result result, U data) Update(U data, IDbCommand command)
        {
            var executenonquery = ExecuteNonQuery(command);

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

            var delete = _commandbuilder.Delete($"{data.Description.Name}", 
                $"{data.Description.Name}",
                _builder.GetWhere(data.Columns.Where(c => c.IsPrimaryKey && c.DbValue != null).ToList(), parameters));

            return Delete(data, delete, CommandType.Text, parameters);
        }
        public virtual (Result result, U data) Delete(U data, (string commandtext, CommandType commandtype, IList<SqlParameter> parameters) dbcommand)
        {
            foreach (var p in _builder.GetParameters(data.Columns.Where(c => c.IsPrimaryKey).ToList(), dbcommand.parameters)) ;

            var command = _creator.GetCommand(dbcommand.commandtext, dbcommand.commandtype, dbcommand.parameters);

            return Delete(data, command);
        }
        public virtual (Result result, U data) Delete(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<SqlParameter> parameters = null)
        {
            return Delete(data, _creator.GetCommand(commandtext, commandtype, parameters));
        }
        public virtual (Result result, U data) Delete(U data, IDbCommand command)
        {
            var executenonquery = ExecuteNonQuery(command);

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
