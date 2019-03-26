using System.Collections.Generic;
using System.Data;

namespace Library.Interface.Entities.Reader
{
    public interface IReaderEntity<T> 
        where T : IEntity
    {
        T CreateInstance();

        T Clear(T entity, int maxdepth = 1, int depth = 0);

        T Read(T entity, IDataReader reader, IList<string> prefixname, string columnseparator, int maxdepth = 1, int depth = 0);
    }
}