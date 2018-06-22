using library.Impl.Data.Command;
using library.Impl.Data.Sql;
using library.Interface.Data;
using library.Interface.Data.Mapper;
using library.Interface.Data.Sql;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace library.Impl.Data
{
    public class Repository<T, U> : BaseRepository<T, U>, IRepository<T, U> 
        where T : IEntity
        where U : ITableRepositoryProperties<T>
    {

        public Repository(ISqlCreator creator, IMapperRepository<T, U> mapper)
            : base(creator, mapper)
        {
        }
        public Repository(IMapperRepository<T, U> mapper, ConnectionStringSettings connectionstringsettings)
            : this(new SqlCreator(connectionstringsettings), mapper)
        {
        }
        public Repository(IMapperRepository<T, U> mapper, string appconnectionstringname)
            : this(mapper, ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appconnectionstringname]])
        {
        }

        public virtual (Result result, IEnumerable<U> datas) ExecuteQuery(string commandtext, CommandType commandtype = CommandType.Text, IList<SqlParameter> parameters = null, int maxdepth = 1, IList<U> datas = null)
        {
            return ExecuteQuery(_creator.GetCommand(commandtext, commandtype, parameters), maxdepth, datas);
        }
    }
}
