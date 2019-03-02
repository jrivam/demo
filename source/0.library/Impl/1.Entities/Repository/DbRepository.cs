using library.Interface.Entities;
using library.Interface.Entities.Reader;
using library.Interface.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Data;

namespace library.Impl.Entities.Repository
{
    public class DbRepository<T> : IDbRepository<T>
        where T : IEntity
    {
        protected readonly IReaderEntity<T> _reader;

        public DbRepository(IReaderEntity<T> reader)
        {
            _reader = reader;
        }

        public virtual (Result result, IEnumerable<T> entities) ExecuteQuery(IDbCommand command, string columnseparator, int maxdepth = 1, IList<T> entities = null)
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

                        _reader.Read(entity, reader, new List<string>(), columnseparator, maxdepth, 0);

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
                return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Exception, $"{ex.Message}{Environment.NewLine}{ex.InnerException}") } }, null);
            }
        } 
    }
}
