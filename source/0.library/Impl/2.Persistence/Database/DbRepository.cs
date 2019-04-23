using Library.Interface.Entities.Reader;
using Library.Interface.Persistence.Database;
using Library.Interface.Persistence.Sql.Builder;
using System;
using System.Collections.Generic;
using System.Data;

namespace Library.Impl.Persistence.Database
{
    public class DbRepository<T> : IDbRepository<T>
    {
        protected readonly IReader<T> _reader;

        public DbRepository(IReader<T> reader)
        {
            _reader = reader;
        }

        public virtual (Result result, IEnumerable<T> entities) ExecuteQuery(IDbCommand command, int maxdepth = 1, IList<T> entities = null)
        {
            var result = new Result() { Success = true };

            try
            {
                var enumeration = default(IList<T>);
                var iterator = (entities ?? new List<T>()).GetEnumerator();

                command.Connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var entity = iterator.MoveNext() ? iterator.Current : _reader.CreateInstance();

                        _reader.Read(entity, reader, new List<string>(), maxdepth, 0);

                        enumeration = enumeration ?? new List<T>();
                        enumeration.Add(entity);
                    }

                    reader.Close();
                }

                command.Connection.Close();

                return (result, enumeration);
            }
            catch (Exception ex)
            {
                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Exception, "ExecuteQuery", $"{ex.Message}{Environment.NewLine}{ex.InnerException}") } }, null);
            }
        } 
    }
}
