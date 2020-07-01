using jrivam.Library.Interface.Entities.Reader;
using jrivam.Library.Interface.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace jrivam.Library.Impl.Persistence.Database
{
    public class DbCommandExecutor : IDbCommandExecutor
    {
        protected readonly IEntityReader _entityreader;

        public DbCommandExecutor(IEntityReader entityreader)
        {
            _entityreader = entityreader;
        }

        public virtual (Result result, IEnumerable<T> entities) ExecuteQuery<T>(IDbCommand command, int maxdepth = 1, ICollection<T> entities = null)
        {
            try
            {
                var enumeration = new Collection<T>();
                var iterator = (entities ?? new Collection<T>()).GetEnumerator();

                command.Connection?.Open();

                using (var datareader = command.ExecuteReader())
                {
                    while (datareader.Read())
                    {
                        var entity = iterator.MoveNext() ? iterator.Current : Entities.HelperEntities<T>.CreateEntity();

                        _entityreader.Read<T>(entity, datareader, new List<string>(), maxdepth, 0);

                        enumeration.Add(entity);
                    }

                    datareader.Close();
                }

                command.Connection?.Close();

                return (new Result(), enumeration);
            }
            catch (Exception ex)
            {
                return (new Result(
                    new ResultMessage()
                    {
                        Category = ResultCategory.Exception,
                        Name = nameof(ExecuteQuery),
                        Description = ex.Message
                    })
                { Exception = ex }, null);
            }
        } 
    }
}
