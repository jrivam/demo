using library.Impl.Data.Sql;
using library.Impl.Data.Sql.Builder;
using library.Interface.Data.Mapper;
using library.Interface.Data.Model;
using library.Interface.Data.Sql;
using library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace library.Impl.Data.Repository
{
    public class RepositoryTable<T, U> : Repository<T, U>, IRepositoryTable<T, U> 
        where T : IEntity
        where U : IEntityTable<T>
    {
        public RepositoryTable(IMapperTable<T, U> mapper, ISqlBuilder<T> builder)
            : base(mapper, builder)
        {
        }
        public RepositoryTable(IMapperTable<T, U> mapper, string connectionstringname)
            : this(mapper, SqlBuilderFactory<T>.GetBuilder(connectionstringname))
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
            if (data.SelectDbCommand != null && UseDbCommand(data.UseDbCommand, data.SelectDbCommand.Value.usedbcommand, usedbcommand))
                return Select(data, data.SelectDbCommand.Value.dbcommand);

            return Select(data, _builder.Select(data));
        }
        public virtual (Result result, U data) Select(U data, (string commandtext, CommandType commandtype, IList<DbParameter> parameters) dbcommand)
        {
            var command = _builder.GetCommand(dbcommand.commandtext, dbcommand.commandtype, dbcommand.parameters);

            foreach (var p in _builder.GetEntityParameters(data.Columns.Where(c => c.IsPrimaryKey).ToList(), command)) ;

            return Select(data, command);
        }
        public virtual (Result result, U data) Select(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<DbParameter> parameters = null)
        {
            return Select(data, _builder.GetCommand(commandtext, commandtype, parameters));
        }
        public virtual (Result result, U data) Select(U data, IDbCommand command)
        {
            var executequery = ExecuteQuery(command, 1, new List<U>() { data });

            return (executequery.result, executequery.datas.FirstOrDefault());
        }

        public virtual (Result result, U data) Insert(U data, bool usedbcommand = false)
        {
            if (data.InsertDbCommand != null && UseDbCommand(data.UseDbCommand, data.InsertDbCommand.Value.usedbcommand, usedbcommand))
                return Insert(data, data.InsertDbCommand.Value.dbcommand);

            return Insert(data, _builder.Insert(data));
        }
        public virtual (Result result, U data) Insert(U data, (string commandtext, CommandType commandtype, IList<DbParameter> parameters) dbcommand)
        {
            var command = _builder.GetCommand(dbcommand.commandtext, dbcommand.commandtype, dbcommand.parameters);

            foreach (var p in _builder.GetEntityParameters(data.Columns.Where(c => !c.IsIdentity).ToList(), command)) ;

            return Insert(data, command);
        }
        public virtual (Result result, U data) Insert(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<DbParameter> parameters = null)
        {
            return Insert(data, _builder.GetCommand(commandtext, commandtype, parameters));
        }
        public virtual (Result result, U data) Insert(U data, IDbCommand command)
        {
            var executescalar = ExecuteScalar(command);

            if (executescalar.result.Success)
            {
                if (executescalar.scalar == null)
                {
                    executescalar.result.Messages.Add((ResultCategory.Information, "Insert: no rows affected"));
                }
                else
                {
                    data.Entity.Id = Convert.ToInt32(executescalar.scalar);
                }
            }

            return (executescalar.result, data);
        }

        public virtual (Result result, U data) Update(U data, bool usedbcommand = false)
        {
            if (data.UpdateDbCommand != null && UseDbCommand(data.UseDbCommand, data.UpdateDbCommand.Value.usedbcommand, usedbcommand))
                return Update(data, data.UpdateDbCommand.Value.dbcommand);

            return Update(data, _builder.Update(data));
        }
        public virtual (Result result, U data) Update(U data, (string commandtext, CommandType commandtype, IList<DbParameter> parameters) dbcommand)
        {
            var command = _builder.GetCommand(dbcommand.commandtext, dbcommand.commandtype, dbcommand.parameters);

            foreach (var p in _builder.GetEntityParameters(data.Columns.ToList(), command)) ;

            return Update(data, command);
        }
        public virtual (Result result, U data) Update(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<DbParameter> parameters = null)
        {
            return Update(data, _builder.GetCommand(commandtext, commandtype, parameters));
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
            if (data.DeleteDbCommand != null && UseDbCommand(data.UseDbCommand, data.DeleteDbCommand.Value.usedbcommand, usedbcommand))
                return Delete(data, data.DeleteDbCommand.Value.dbcommand);

            return Delete(data, _builder.Delete(data));
        }
        public virtual (Result result, U data) Delete(U data, (string commandtext, CommandType commandtype, IList<DbParameter> parameters) dbcommand)
        {
            var command = _builder.GetCommand(dbcommand.commandtext, dbcommand.commandtype, dbcommand.parameters);

            foreach (var p in _builder.GetEntityParameters(data.Columns.Where(c => c.IsPrimaryKey).ToList(), command)) ;

            return Delete(data, command);
        }
        public virtual (Result result, U data) Delete(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<DbParameter> parameters = null)
        {
            return Delete(data, _builder.GetCommand(commandtext, commandtype, parameters));
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
