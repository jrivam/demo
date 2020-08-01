using System.Collections.Generic;
using System.Data;

namespace jrivam.Library.Interface.Entities.Reader
{
    public interface IEntityReader
    {
        void Clear<T>(T entity);

        T Read<T>(T entity, IDataReader datareader, IList<string> prefixname, int maxdepth = 1, int depth = 0);
    }
}