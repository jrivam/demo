using library.Impl.Data.Sql;
using library.Impl.Data.Sql.Builder;
using library.Interface.Data;
using library.Interface.Data.Sql;
using library.Interface.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace library.Impl.Data.Repository
{
    public class Repository<T, U> : IRepository<T, U> where T : IEntity
                                                     where U : IEntityTable<T>
    {
        protected readonly IMapperTable<T, U> _mapper;
        protected readonly ISqlBuilder<T> _builder;

        public Repository(IMapperTable<T, U> mapper, ISqlBuilder<T> builder)
        {
            _mapper = mapper;
            _builder = builder;
        }
        public Repository(IMapperTable<T, U> mapper, string connectionstringname)
            : this(mapper, SqlBuilderFactory<T>.GetBuilder(connectionstringname))
        {
        }

        public virtual U Clear(U data, int maxdepth = 1)
        {
            return _mapper.Clear(data, maxdepth, 0);
        }

        public virtual (Result result, U data) SelectSingle(IQueryTable query, int maxdepth = 1)
        {
            return SelectSingle(_builder.Select(query, maxdepth, 1));
        }
        public virtual (Result result, U data) SelectSingle(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null, int maxdepth = 1)
        {
            return SelectSingle(_builder.GetCommand(commandtext, commandtype, parameters), maxdepth);
        }
        public virtual (Result result, U data) SelectSingle(IDbCommand command, int maxdepth = 1)
        {
            var executequery = ExecuteQuery(command, maxdepth);

            return (executequery.result, executequery.datas.FirstOrDefault());
        }

        public virtual (Result result, IEnumerable<U> datas) SelectMultiple(IQueryTable query, int maxdepth = 1, int top = 0)
        {
            return SelectMultiple(_builder.Select(query, maxdepth, top), maxdepth);
        }
        public virtual (Result result, IEnumerable<U> datas) SelectMultiple(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null, int maxdepth = 1)
        {
            return SelectMultiple(_builder.GetCommand(commandtext, commandtype, parameters), maxdepth);
        }
        public virtual (Result result, IEnumerable<U> datas) SelectMultiple(IDbCommand command, int maxdepth = 1)
        {
            return ExecuteQuery(command, maxdepth);
        }

        public virtual (Result result, int rows) Update(U table, IQueryTable query, int maxdepth = 1)
        {
            return Update(_builder.Update(table, query, maxdepth));
        }
        public virtual (Result result, int rows) Update(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null)
        {
            return Update(_builder.GetCommand(commandtext, commandtype, parameters));
        }
        public virtual (Result result, int rows) Update(IDbCommand command)
        {
            return ExecuteNonQuery(command);
        }

        public virtual (Result result, int rows) Delete(IQueryTable query, int maxdepth = 1)
        {
            return Delete(_builder.Delete(query, maxdepth));
        }
        public virtual (Result result, int rows) Delete(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null)
        {
            return Delete(_builder.GetCommand(commandtext, commandtype, parameters));
        }
        public virtual (Result result, int rows) Delete(IDbCommand command)
        {
            return ExecuteNonQuery(command);
        }

        public virtual (Result result, U data) Select(U data)
        {
            return (data.SelectDbCommand != null) ? Select(data, data.SelectDbCommand.Value) : Select(data, _builder.Select(data));
        }
        public virtual (Result result, U data) Select(U data, (string commandtext, CommandType commandtype, IList<DbParameter> parameters) entitycommand)
        {
            var command = _builder.GetCommand(entitycommand.commandtext, entitycommand.commandtype, entitycommand.parameters);

            foreach (var p in _builder.GetEntityParameters(data.Columns.Where(c => c.IsPrimaryKey).ToList(), command));

            return Select(data, command);
        }
        public virtual (Result result, U data) Select(U data, string commandtext, CommandType commandtype = CommandType.StoredProcedure, IList<DbParameter> parameters = null)
        {
            return Select(data, _builder.GetCommand(commandtext, commandtype, parameters));
        }
        public virtual (Result result, U data) Select(U data, IDbCommand command)
        {
            if (data.Domain.Id != null)
            {
                var executequery = ExecuteQuery(command, 1, new List<U>() { data });

                return (executequery.result, executequery.datas.FirstOrDefault());
            }

            return (new Result() { Success = true, Messages = new List<(ResultCategory, string)>() { (ResultCategory.Information, "Select: empty primary key") } }, data);
        }

        public virtual (Result result, U data) Insert(U data)
        {
            return (data.InsertDbCommand != null) ? Insert(data, data.InsertDbCommand.Value) : Insert(data, _builder.Insert(data));
        }
        public virtual (Result result, U data) Insert(U data, (string commandtext, CommandType commandtype, IList<DbParameter> parameters) entitycommand)
        {
            var command = _builder.GetCommand(entitycommand.commandtext, entitycommand.commandtype, entitycommand.parameters);

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

            if (executescalar.result != null)
            {
                data.Domain.Id = Convert.ToInt32(executescalar.scalar);
            }
            else
            {
                executescalar.result.Passed = false;
                executescalar.result.Messages.Add((ResultCategory.Information, "Insert: no rows affected"));
            }

            return (executescalar.result, data);
        }

        public virtual (Result result, U data) Update(U data)
        {
            return (data.UpdateDbCommand != null) ? Update(data, data.UpdateDbCommand.Value) : Update(data, _builder.Update(data));
        }
        public virtual (Result result, U data) Update(U data, (string commandtext, CommandType commandtype, IList<DbParameter> parameters) entitycommand)
        {
            var command = _builder.GetCommand(entitycommand.commandtext, entitycommand.commandtype, entitycommand.parameters);

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

            if (executenonquery.rows > 0)
            {

            }
            else
            {
                executenonquery.result.Passed = false;
                executenonquery.result.Messages.Add((ResultCategory.Information, "Update: no rows affected"));
            }

            return (executenonquery.result, data);
        }

        public virtual (Result result, U data) Delete(U data)
        {
            return (data.DeleteDbCommand != null) ? Delete(data, data.DeleteDbCommand.Value) : Delete(data, _builder.Delete(data));
        }
        public virtual (Result result, U data) Delete(U data, (string commandtext, CommandType commandtype, IList<DbParameter> parameters) entitycommand)
        {
            var command = _builder.GetCommand(entitycommand.commandtext, entitycommand.commandtype, entitycommand.parameters);

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

            if (executenonquery.rows > 0)
            {
            }
            else
            {
                executenonquery.result.Passed = false;
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

                return (new Result() { Success = true, Passed = true }, rows);
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

                return (new Result() { Success = true, Passed = true }, scalar);
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
                var list = new List<U>();

                command.Connection.Open();
                var reader = command.ExecuteReader();

                bool passed = false;

                var iterator = datas?.GetEnumerator() ?? new List<U>().GetEnumerator();
                while (reader.Read())
                {
                    var data = (iterator.MoveNext() ? iterator.Current : _mapper.CreateInstance(maxdepth, 0));

                    _mapper.Clear(data, maxdepth, 0);

                    _mapper.Read(data, reader, new List<string>(), _builder.SyntaxSign.AliasSeparatorColumn, maxdepth, 0);

                    _mapper.Map(data, maxdepth, 0);

                    list.Add(data);

                    passed = true;
                }

                reader.Close();
                command.Connection.Close();

                return (new Result() { Success = true, Passed = passed }, list);
            }
            catch (Exception ex)
            {
                return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Exception, ex.Message) } }, null);
            }
        }

        //public virtual void GetFromReader(U data, IDataReader reader, IList<string> prefixname, string aliasseparator, int maxdepth = 1, int depth = 0)
        //{
        //    prefixname.Add(data.Reference);

        //    var columprefix = string.Join(aliasseparator, prefixname);
        //    columprefix = $"{columprefix}{(columprefix == "" ? "" : aliasseparator)}";

        //    for (int index = 0; index < reader.FieldCount; index++)
        //    {
        //        var columnname = reader.GetName(index).Replace(columprefix, string.Empty);

        //        data[columnname].DbValue = reader[$"{columprefix}{columnname}"];
        //    }
        //}
    }
}
