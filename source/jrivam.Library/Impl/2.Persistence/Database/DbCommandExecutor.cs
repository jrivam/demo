using jrivam.Library.Interface.Entities.Reader;
using jrivam.Library.Interface.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace jrivam.Library.Impl.Persistence.Database
{
    public class DbCommandExecutor<T> : IDbCommandExecutor<T>
    {
        protected readonly IReader<T> _reader;

        public DbCommandExecutor(IReader<T> reader)
        {
            _reader = reader;
        }

        public virtual (Result result, IEnumerable<T> entities) ExecuteQuery(IDbCommand command, int maxdepth = 1, ICollection<T> entities = null)
        {
            var result = new Result() { Success = true };

            try
            {
                var enumeration = new Collection<T>();
                var iterator = (entities ?? new Collection<T>()).GetEnumerator();

                command.Connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var entity = iterator.MoveNext() ? iterator.Current : Entities.HelperEntities<T>.CreateEntity();

                        _reader.Read(entity, reader, new List<string>(), maxdepth, 0);

                        enumeration.Add(entity);
                    }

                    reader.Close();
                }

                command.Connection.Close();

                return (result, enumeration);
            }
            catch (Exception ex)
            {
                return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Exception, nameof(ExecuteQuery), $"{ex.Message}{Environment.NewLine}{ex.InnerException}") } }, null);
            }
        } 
    }
}
