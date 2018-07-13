using library.Interface.Data.Mapper;
using library.Interface.Data.Sql;
using library.Interface.Data.Table;
using library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace library.Impl.Data
{
    public class BaseRepository<T, U> : RepositoryBulk
        where T : IEntity
        where U : ITableRepositoryProperties<T>
    {
        protected readonly IMapperRepository<T, U> _mapper;

        public BaseRepository(ISqlCreator creator, IMapperRepository<T, U> mapper)
            : base(creator)
        {
            _mapper = mapper;
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
                        var data = iterator.MoveNext() ? iterator.Current : _mapper.CreateInstance(maxdepth, 0);

                        _mapper.Clear(data, maxdepth, 0);
                        _mapper.Read(data, reader, new List<string>(), maxdepth, 0);
                        _mapper.Map(data, maxdepth, 0);

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
