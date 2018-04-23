using library.Impl.Data.Sql;
using library.Impl.Data.Sql.Builder;
using library.Interface.Data.Mapper;
using library.Interface.Data.Model;
using library.Interface.Data.Query;
using library.Interface.Data.Sql;
using library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace library.Impl.Data.Repository
{
    public class RepositoryQuery<T, U> : Repository<T, U>, IRepositoryQuery<T, U> 
        where T : IEntity
        where U : IEntityTable<T>
    {
        public RepositoryQuery(IMapperTable<T, U> mapper, ISqlBuilder<T> builder)
            : base(mapper, builder)
        {
        }
        public RepositoryQuery(IMapperTable<T, U> mapper, string connectionstringname)
            : this(mapper, SqlBuilderFactory<T>.GetBuilder(connectionstringname))
        {
        }

        public virtual (Result result, U data) SelectSingle(IQueryTable querytable, int maxdepth = 1, U data = default(U))
        {
            return SelectSingle(_builder.Select(querytable, maxdepth, 1), maxdepth, data);
        }
        public virtual (Result result, U data) SelectSingle(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null, int maxdepth = 1, U data = default(U))
        {
            return SelectSingle(_builder.GetCommand(commandtext, commandtype, parameters), maxdepth, data);
        }
        public virtual (Result result, U data) SelectSingle(IDbCommand command, int maxdepth = 1, U data = default(U))
        {
            var executequery = ExecuteQuery(command, maxdepth, new List<U> { data });

            return (executequery.result, executequery.datas.FirstOrDefault());
        }

        public virtual (Result result, IEnumerable<U> datas) SelectMultiple(IQueryTable querytable, int maxdepth = 1, int top = 0, IList<U> datas = null)
        {
            return SelectMultiple(_builder.Select(querytable, maxdepth, top), maxdepth, datas);
        }
        public virtual (Result result, IEnumerable<U> datas) SelectMultiple(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null, int maxdepth = 1, IList<U> datas = null)
        {
            return SelectMultiple(_builder.GetCommand(commandtext, commandtype, parameters), maxdepth, datas);
        }
        public virtual (Result result, IEnumerable<U> datas) SelectMultiple(IDbCommand command, int maxdepth = 1, IList<U> datas = null)
        {
            return ExecuteQuery(command, maxdepth, datas);
        }

        public virtual (Result result, int rows) Update(U table, IQueryTable querytable, int maxdepth = 1)
        {
            return Update(_builder.Update(table, querytable, maxdepth));
        }
        public virtual (Result result, int rows) Update(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null)
        {
            return Update(_builder.GetCommand(commandtext, commandtype, parameters));
        }
        public virtual (Result result, int rows) Update(IDbCommand command)
        {
            return ExecuteNonQuery(command);
        }

        public virtual (Result result, int rows) Delete(IQueryTable querytable, int maxdepth = 1)
        {
            return Delete(_builder.Delete(querytable, maxdepth));
        }
        public virtual (Result result, int rows) Delete(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null)
        {
            return Delete(_builder.GetCommand(commandtext, commandtype, parameters));
        }
        public virtual (Result result, int rows) Delete(IDbCommand command)
        {
            return ExecuteNonQuery(command);
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

            if (executescalar.scalar == null)
            {
                executescalar.result.Success = false;
                executescalar.result.Messages.Add((ResultCategory.Information, "Insert: no rows affected"));
            }
            else
            {
                data.Entity.Id = Convert.ToInt32(executescalar.scalar);
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

            if (executenonquery.rows <= 0)
            {
                executenonquery.result.Success = false;
                executenonquery.result.Messages.Add((ResultCategory.Information, "Update: no rows affected"));
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

            if (executenonquery.rows <= 0)
            {
                executenonquery.result.Success = false;
                executenonquery.result.Messages.Add((ResultCategory.Information, "Delete: no rows affected"));
            }

            return (executenonquery.result, data);
        }

        public virtual (Result result, int rows) ExecuteNonQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null)
        {
            return ExecuteNonQuery(_builder.GetCommand(commandtext, commandtype, parameters));
        }
        public virtual (Result result, int rows) ExecuteNonQuery(IDbCommand command)
        {
            try
            {
                command.Connection.Open();

                int rows = command.ExecuteNonQuery();

                command.Connection.Close();

                return (new Result() { Success = true }, rows);
            }
            catch (Exception ex)
            {
                return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Exception, ex.Message) } }, -1);
            }
        }

        public virtual (Result result, object scalar) ExecuteScalar(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null)
        {
            return ExecuteScalar(_builder.GetCommand(commandtext, commandtype, parameters));
        }
        public virtual (Result result, object scalar) ExecuteScalar(IDbCommand command)
        {
            try
            {
                command.Connection.Open();

                object scalar = command.ExecuteScalar();

                command.Connection.Close();

                return (new Result() { Success = true }, scalar);
            }
            catch (Exception ex)
            {
                return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Exception, ex.Message) } }, null);
            }
        }

        public virtual (Result result, IEnumerable<U> datas) ExecuteQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null, int maxdepth = 1, IList<U> datas = null)
        {
            return ExecuteQuery(_builder.GetCommand(commandtext, commandtype, parameters), maxdepth, datas);
        }
        public virtual (Result result, IEnumerable<U> datas) ExecuteQuery(IDbCommand command, int maxdepth = 1, IList<U> datas = null)
        {
            try
            {
                var enumeration = new List<U>();
                var iterator = (datas ?? new List<U>()).GetEnumerator();

                command.Connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var data = iterator.MoveNext() ? iterator.Current : _mapper.CreateInstance(maxdepth);

                        _mapper.Clear(data, maxdepth);
                        _mapper.Read(data, reader, new List<string>(), _builder.SyntaxSign.AliasSeparatorColumn, maxdepth);
                        _mapper.Map(data, maxdepth);

                        enumeration.Add(data);
                    }

                    reader.Close();
                }

                command.Connection.Close();

                return (new Result() { Success = true }, enumeration);
            }
            catch (Exception ex)
            {
                return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Exception, ex.Message) } }, null);
            }
        } 
    }
}
