using library.Impl.Data.Sql;
using library.Interface.Data;
using library.Interface.Data.Mapper;
using library.Interface.Data.Model;
using library.Interface.Data.Sql;
using library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace library.Impl.Data
{
    public class Repository<T, U> : IRepository<T, U> 
        where T : IEntity
        where U : IEntityTable<T>
    {
        protected readonly IMapperTable<T, U> _mapper;
        protected readonly ISqlBuilder<T> _builder;

        public Repository(IMapperTable<T, U> mapper, ISqlBuilder<T> builder)
        {
            _mapper = mapper;
            _builder = builder;
        }
        
        public virtual (Result result, int rows) ExecuteNonQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<DbParameter> parameters = null)
        {
            return ExecuteNonQuery(_builder.GetCommand(commandtext, commandtype, parameters));
        }
        public virtual (Result result, int rows) ExecuteNonQuery(IDbCommand command)
        {
            var result = new Result() { Success = true };

            try
            {
                command.Connection.Open();

                int rows = command.ExecuteNonQuery();

                command.Connection.Close();

                return (result, rows);
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
            var result = new Result() { Success = true };

            try
            {
                command.Connection.Open();

                object scalar = command.ExecuteScalar();

                command.Connection.Close();

                return (result, scalar);
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
            var result = new Result() { Success = true };

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

                return (result, enumeration);
            }
            catch (Exception ex)
            {
                return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Exception, ex.Message) } }, null);
            }
        } 
    }
}
