using library.Impl.Data.Sql;
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
        protected readonly ISqlBuilderTable<T> _builder;

        public RepositoryTable(ISqlCreator creator, IMapperRepository<T, U> mapper, ISqlBuilderTable<T> builder)
            : base(creator, mapper)
        {
            _builder = builder;
        }
        public RepositoryTable(IMapperRepository<T, U> mapper, ConnectionStringSettings connectionstringsettings)
            : this(new SqlCreator(connectionstringsettings), mapper, SqlBuilderTableFactory<T>.Create(connectionstringsettings))
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

            var select = _builder.Select(data);
            return Select(data, select.commandtext, CommandType.Text, select.parameters);
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
            var executequery = ExecuteQuery(command, 1, data == null ? default(List<U>) : new List<U> { data });

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

            var insert = _builder.Insert(data);
            return Insert(data, insert.commandtext, CommandType.Text, insert.parameters);
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

            var update = _builder.Update(data);
            return Update(data, update.commandtext, CommandType.Text, update.parameters);
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

            var delete = _builder.Delete(data);
            return Delete(data, delete.commandtext, CommandType.Text, delete.parameters);
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
