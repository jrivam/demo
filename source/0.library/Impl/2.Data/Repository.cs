using library.Impl.Data.Sql;
using library.Interface.Data;
using library.Interface.Data.Mapper;
using library.Interface.Data.Sql;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace library.Impl.Data
{
    public class Repository<T, U> : RepositoryBase, IRepository<T, U> 
        where T : IEntity
        where U : IEntityRepositoryProperties<T>
    {
        protected readonly IMapperRepository<T, U> _mapper;

        public Repository(ISqlCreator creator, IMapperRepository<T, U> mapper)
            : base (creator)
        {
            _mapper = mapper;
        }
        public Repository(IMapperRepository<T, U> mapper, ConnectionStringSettings connectionstringsettings)
            : this(new SqlCreator(connectionstringsettings), mapper)
        {
        }
        public Repository(IMapperRepository<T, U> mapper, string connectionstringname)
            : this(mapper, ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[connectionstringname]])
        {
        }

        public virtual (Result result, IEnumerable<U> datas) ExecuteQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, int maxdepth = 1, IList<U> datas = null)
        {
            return ExecuteQuery(_creator.GetCommand(commandtext, commandtype, parameters), maxdepth, datas);
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
                        _mapper.Read(data, reader, new List<string>(), maxdepth);
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
                return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Exception, $"{ex.Message}{Environment.NewLine}{ex.InnerException}") } }, null);
            }
        } 
    }
}
