using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Interface.Entities.Reader
{
    public interface IReader<T>
    {
        T Clear(T entity);

        T Read(T entity, IDataReader reader, IList<string> prefixname, int maxdepth = 1, int depth = 0);
    }
}